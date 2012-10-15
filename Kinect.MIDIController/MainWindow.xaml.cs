using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

using Coding4Fun.Kinect.Wpf;
using Microsoft.Research.Kinect.Nui;
using MIDIWrapper;


using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;
using System.Windows.Controls.Primitives;


namespace Kinect.MIDIController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Global Constants and Variables
        private NotifyIcon _notifyIcon = new NotifyIcon();

        private const float SkeletonMaxX = 0.60f;
        private const float SkeletonMaxY = 0.40f;

        private const float Smoothing = 0.7f;
        private const float Correction = 0.3f;
        private const float Prediction = 0.4f;
        private const float JitterRadius = 1.0f;
        private const float MaxDeviationRadius = 0.5f;

        bool sendSignals = false;

        private Runtime _runtime = new Runtime();


        Dictionary<JointID, Brush> jointColors = new Dictionary<JointID, Brush>() { 
            {JointID.HipCenter, new SolidColorBrush(Color.FromRgb(169, 176, 155))},
            {JointID.Spine, new SolidColorBrush(Color.FromRgb(169, 176, 155))},
            {JointID.ShoulderCenter, new SolidColorBrush(Color.FromRgb(168, 230, 29))},
            {JointID.Head, new SolidColorBrush(Color.FromRgb(200, 0,   0))},
            {JointID.ShoulderLeft, new SolidColorBrush(Color.FromRgb(79,  84,  33))},
            {JointID.ElbowLeft, new SolidColorBrush(Color.FromRgb(84,  33,  42))},
            {JointID.WristLeft, new SolidColorBrush(Color.FromRgb(255, 126, 0))},
            {JointID.HandLeft, new SolidColorBrush(Color.FromRgb(215,  86, 0))},
            {JointID.ShoulderRight, new SolidColorBrush(Color.FromRgb(33,  79,  84))},
            {JointID.ElbowRight, new SolidColorBrush(Color.FromRgb(33,  33,  84))},
            {JointID.WristRight, new SolidColorBrush(Color.FromRgb(77,  109, 243))},
            {JointID.HandRight, new SolidColorBrush(Color.FromRgb(37,   69, 243))},
            {JointID.HipLeft, new SolidColorBrush(Color.FromRgb(77,  109, 243))},
            {JointID.KneeLeft, new SolidColorBrush(Color.FromRgb(69,  33,  84))},
            {JointID.AnkleLeft, new SolidColorBrush(Color.FromRgb(229, 170, 122))},
            {JointID.FootLeft, new SolidColorBrush(Color.FromRgb(255, 126, 0))},
            {JointID.HipRight, new SolidColorBrush(Color.FromRgb(181, 165, 213))},
            {JointID.KneeRight, new SolidColorBrush(Color.FromRgb(71, 222,  76))},
            {JointID.AnkleRight, new SolidColorBrush(Color.FromRgb(245, 228, 156))},
            {JointID.FootRight, new SolidColorBrush(Color.FromRgb(77,  109, 243))}
        };

        Instrument instrument = null;

        string OutputDeviceName = null;
        
        byte ChannelLX;
        byte ControllerLX;
        byte valueLX = 0;

        byte ChannelLY;
        byte ControllerLY;
        byte valueLY = 0;

        byte ChannelRX;
        byte ControllerRX;
        byte valueRX = 0;

        byte ChannelRY;
        byte ControllerRY;
        byte valueRY = 0;

        #endregion

        #region Form Events

        public MainWindow()
        {
            InitializeComponent();
            InitialiseSystemTrayButton();
            InitialiseControls();
        }

        private void InitialiseSystemTrayButton()
        {
            // create tray icon
            _notifyIcon.Icon = new System.Drawing.Icon("Kinect.ico");
            _notifyIcon.Visible = true;
            _notifyIcon.DoubleClick += delegate
            {
                if (this.WindowState == System.Windows.WindowState.Normal)
                {
                    this.WindowState = System.Windows.WindowState.Minimized;
                    this.ShowInTaskbar = false;
                }
                else
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                    this.Focus();
                    this.ShowInTaskbar = true;
                }
            };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            _runtime.DepthFrameReady += new EventHandler<ImageFrameReadyEventArgs>(_runtime_DepthFrameReady);
            _runtime.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(_runtime_SkeletonFrameReady);
            _runtime.VideoFrameReady += new EventHandler<ImageFrameReadyEventArgs>(_runtime_VideoFrameReady);

            try
            {
                // tell Kinect we need the depth buffer and skeletal tracking
                _runtime.Initialize(RuntimeOptions.UseDepth | RuntimeOptions.UseSkeletalTracking | RuntimeOptions.UseColor);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Could not initialize Kinect device: " + ex.Message);
            }

            // parameters used to smooth the skeleton data
            _runtime.SkeletonEngine.TransformSmooth = true;
            TransformSmoothParameters parameters = new TransformSmoothParameters();
            parameters.Smoothing = Smoothing;
            parameters.Correction = Correction;
            parameters.Prediction = Prediction;
            parameters.JitterRadius = JitterRadius;
            parameters.MaxDeviationRadius = MaxDeviationRadius;
            _runtime.SkeletonEngine.SmoothParameters = parameters;

            try
            {
                // open the depth stream at the proper resolution
                _runtime.DepthStream.Open(ImageStreamType.Depth, 2, ImageResolution.Resolution320x240, ImageType.Depth);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open depth stream: " + ex.Message);
            }

            try
            {
                _runtime.VideoStream.Open(ImageStreamType.Video, 2, ImageResolution.Resolution640x480, ImageType.Color);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open Video Stream" + ex.Message);
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {

            // shut down the Kinect device
            _notifyIcon.Visible = false;
            _runtime.Uninitialize();

            instrument.Close();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {

            if (sendSignals == false)
            {
                #region Input Validation
                if (cmbMIDIDevice.SelectedIndex == -1)
                {
                    MessageBox.Show("Please set the Kinect Virtual MIDI Device");
                    return;
                }


                if (GetAlreadySelectedControllerValues().Count == 0)
                {
                    MessageBox.Show("Please select at least one controller");
                    return;
                }

                OutputDeviceName = cmbMIDIDevice.SelectedValue.ToString();

                ChannelLX = cmbLXChannel.SelectedIndex != -1 ? Convert.ToByte(cmbLXChannel.SelectedValue) : Convert.ToByte(0);
                ChannelLY = cmbLYChannel.SelectedIndex != -1 ? Convert.ToByte(cmbLYChannel.SelectedValue) : Convert.ToByte(0);
                ChannelRX = cmbRXChannel.SelectedIndex != -1 ? Convert.ToByte(cmbRXChannel.SelectedValue) : Convert.ToByte(0);
                ChannelRY = cmbRYChannel.SelectedIndex != -1 ? Convert.ToByte(cmbRYChannel.SelectedValue) : Convert.ToByte(0);



                ControllerLX = cmbLXController.SelectedIndex != -1 ? Convert.ToByte(cmbLXController.SelectedValue) : Convert.ToByte(0);
                ControllerLY = cmbLYController.SelectedIndex != -1 ? Convert.ToByte(cmbLYController.SelectedValue) : Convert.ToByte(0);
                ControllerRX = cmbRXController.SelectedIndex != -1 ? Convert.ToByte(cmbRXController.SelectedValue) : Convert.ToByte(0);
                ControllerRY = cmbRYController.SelectedIndex != -1 ? Convert.ToByte(cmbRYController.SelectedValue) : Convert.ToByte(0);

                #endregion



                InitialiseMIDIDevice();


                btnStart.Content = "Stop Sending MIDI Signals";
                btnStart.Background = Brushes.Red;

                FreezeControls(false);
                sendSignals = true;



            }
            else
            {
                sendSignals = false;
                btnStart.Content = "Start Sending MIDI Signals";
                btnStart.Background = Brushes.Green;

                CloseMIDIDevice();

                InitialiseControls();

                FreezeControls(true);
                sendSignals = false;
            }


        }






        #endregion

        #region Kinect Event Handlers
        void _runtime_VideoFrameReady(object sender, ImageFrameReadyEventArgs e)
        {
            if (chkDisplayVideo.IsChecked.Value)
            {
                PlanarImage imageData = e.ImageFrame.Image;
                imgVideoFrame.Source = BitmapSource.Create(imageData.Width, imageData.Height, 96, 96, PixelFormats.Bgr32, null, imageData.Bits, imageData.Width * imageData.BytesPerPixel);
            }
        }

        void _runtime_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            if (chkDisplaySkeleton.IsChecked.Value)
            {
                DisplaySkeleton(e);
            }

            if (sendSignals)
            {
                SendMIDISignals(e);
            }

        }


        void _runtime_DepthFrameReady(object sender, ImageFrameReadyEventArgs e)
        {
            if (chkDisplayDepth.IsChecked.Value)
            {
                if (this.WindowState == WindowState.Normal)
                {
                    imgDepthFrame.Source = e.ImageFrame.ToBitmapSource();
                }
            }
        }
        #endregion

        #region Methods to Render Skeleton
        private void DisplaySkeleton(SkeletonFrameReadyEventArgs e)
        {
            SkeletonFrame skeletonFrame = e.SkeletonFrame;
            int iSkeleton = 0;
            Brush[] brushes = new Brush[6];
            brushes[0] = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            brushes[1] = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            brushes[2] = new SolidColorBrush(Color.FromRgb(64, 255, 255));
            brushes[3] = new SolidColorBrush(Color.FromRgb(255, 255, 64));
            brushes[4] = new SolidColorBrush(Color.FromRgb(255, 64, 255));
            brushes[5] = new SolidColorBrush(Color.FromRgb(128, 128, 255));

            imgSkeletonFrame.Children.Clear();
            foreach (SkeletonData data in skeletonFrame.Skeletons)
            {
                if (SkeletonTrackingState.Tracked == data.TrackingState)
                {
                    // Draw bones
                    Brush brush = brushes[iSkeleton % brushes.Length];
                    imgSkeletonFrame.Children.Add(getBodySegment(data.Joints, brush, JointID.HipCenter, JointID.Spine, JointID.ShoulderCenter, JointID.Head));
                    imgSkeletonFrame.Children.Add(getBodySegment(data.Joints, brush, JointID.ShoulderCenter, JointID.ShoulderLeft, JointID.ElbowLeft, JointID.WristLeft, JointID.HandLeft));
                    imgSkeletonFrame.Children.Add(getBodySegment(data.Joints, brush, JointID.ShoulderCenter, JointID.ShoulderRight, JointID.ElbowRight, JointID.WristRight, JointID.HandRight));
                    imgSkeletonFrame.Children.Add(getBodySegment(data.Joints, brush, JointID.HipCenter, JointID.HipLeft, JointID.KneeLeft, JointID.AnkleLeft, JointID.FootLeft));
                    imgSkeletonFrame.Children.Add(getBodySegment(data.Joints, brush, JointID.HipCenter, JointID.HipRight, JointID.KneeRight, JointID.AnkleRight, JointID.FootRight));

                    // Draw joints
                    foreach (Joint joint in data.Joints)
                    {
                        Point jointPos = getDisplayPosition(joint);
                        Line jointLine = new Line();
                        jointLine.X1 = jointPos.X - 3;
                        jointLine.X2 = jointLine.X1 + 6;
                        jointLine.Y1 = jointLine.Y2 = jointPos.Y;
                        jointLine.Stroke = jointColors[joint.ID];
                        jointLine.StrokeThickness = 6;
                        imgSkeletonFrame.Children.Add(jointLine);
                    }
                }
                iSkeleton++;
            } // for each skeleton
        }

        Polyline getBodySegment(Microsoft.Research.Kinect.Nui.JointsCollection joints, Brush brush, params JointID[] ids)
        {
            PointCollection points = new PointCollection(ids.Length);
            for (int i = 0; i < ids.Length; ++i)
            {
                points.Add(getDisplayPosition(joints[ids[i]]));
            }

            Polyline polyline = new Polyline();
            polyline.Points = points;
            polyline.Stroke = brush;
            polyline.StrokeThickness = 5;
            return polyline;
        }

        private Point getDisplayPosition(Joint joint)
        {
            float depthX, depthY;
            _runtime.SkeletonEngine.SkeletonToDepthImage(joint.Position, out depthX, out depthY);
            depthX = Math.Max(0, Math.Min(depthX * 320, 320));  //convert to 320, 240 space
            depthY = Math.Max(0, Math.Min(depthY * 240, 240));  //convert to 320, 240 space
            int colorX, colorY;
            ImageViewArea iv = new ImageViewArea();
            // only ImageResolution.Resolution640x480 is supported at this point
            _runtime.NuiCamera.GetColorPixelCoordinatesFromDepthPixel(ImageResolution.Resolution640x480, iv, (int)depthX, (int)depthY, (short)0, out colorX, out colorY);

            // map back to skeleton.Width & skeleton.Height
            return new Point((int)(imgSkeletonFrame.Width * colorX / 640.0), (int)(imgSkeletonFrame.Height * colorY / 480));
        }
        #endregion

        #region Combo Box Event Handlers

        


        #endregion

        #region UI Helper Methods

        private void InitialiseControls()
        {
            cmbMIDIDevice.ItemsSource = Instrument.OutDeviceNames();

            cmbLXChannel.ItemsSource = Enumerable.Range(1, 16);
            cmbLYChannel.ItemsSource = Enumerable.Range(1, 16);
            cmbRXChannel.ItemsSource = Enumerable.Range(1, 16);
            cmbRYChannel.ItemsSource = Enumerable.Range(1, 16);

            cmbLXController.ItemsSource = Enumerable.Range(1, 127);
            cmbLYController.ItemsSource = Enumerable.Range(1, 127);
            cmbRXController.ItemsSource = Enumerable.Range(1, 127);
            cmbRYController.ItemsSource = Enumerable.Range(1, 127);

            cmbLXChannel.SelectedIndex = -1;
            cmbLYChannel.SelectedIndex = -1;
            cmbRXChannel.SelectedIndex = -1;
            cmbRYChannel.SelectedIndex = -1;

            cmbLXController.SelectedIndex = -1;
            cmbLYController.SelectedIndex = -1;
            cmbRXController.SelectedIndex = -1;
            cmbRYController.SelectedIndex = -1;

            sliderLX.Value = 0;
            sliderLY.Value = 0;
            sliderRX.Value = 0;
            sliderRY.Value = 0;



        }

        private void CheckChannelConflicts(System.Windows.Controls.ComboBox comboBox, object SelectedValue)
        {
            int selectedValue = (int)SelectedValue;

            Dictionary<string, int> alreadySelectedValues = GetAlreadySelectedChannelValues();

            alreadySelectedValues.Remove(comboBox.Name);

            if (alreadySelectedValues.Values.Contains(selectedValue))
            {
                MessageBox.Show("This value is already being used.", "Conflict", MessageBoxButton.OK, MessageBoxImage.Stop);
                comboBox.SelectedIndex = -1;
            }

        }

        private void CheckControllerConflicts(System.Windows.Controls.ComboBox comboBox, object SelectedValue)
        {
            int selectedValue = (int)SelectedValue;

            Dictionary<string, int> alreadySelectedValues = GetAlreadySelectedControllerValues();

            alreadySelectedValues.Remove(comboBox.Name);

            if (alreadySelectedValues.Values.Contains(selectedValue))
            {
                MessageBox.Show("This value is already being used.", "Conflict", MessageBoxButton.OK, MessageBoxImage.Stop);
                comboBox.SelectedIndex = -1;
            }

        }

        private Dictionary<string, int> GetAlreadySelectedChannelValues()
        {
            Dictionary<string, int> alreadySelectedValues = new Dictionary<string, int>();

            if (cmbLXChannel.SelectedIndex != -1)
            {
                alreadySelectedValues.Add(cmbLXChannel.Name, (int)cmbLXChannel.SelectedValue);
            }

            if (cmbLYChannel.SelectedIndex != -1)
            {
                alreadySelectedValues.Add(cmbLYChannel.Name, (int)cmbLYChannel.SelectedValue);
            }



            if (cmbRXChannel.SelectedIndex != -1)
            {
                alreadySelectedValues.Add(cmbRXChannel.Name, (int)cmbRXChannel.SelectedValue);
            }

            if (cmbRYChannel.SelectedIndex != -1)
            {
                alreadySelectedValues.Add(cmbRYChannel.Name, (int)cmbRYChannel.SelectedValue);
            }

            return alreadySelectedValues;
        }

        private Dictionary<string, int> GetAlreadySelectedControllerValues()
        {
            Dictionary<string, int> alreadySelectedValues = new Dictionary<string, int>();

            if (cmbLXController.SelectedIndex != -1)
            {
                alreadySelectedValues.Add(cmbLXController.Name, (int)cmbLXController.SelectedValue);
            }

            if (cmbLYController.SelectedIndex != -1)
            {
                alreadySelectedValues.Add(cmbLYController.Name, (int)cmbLYController.SelectedValue);
            }



            if (cmbRXController.SelectedIndex != -1)
            {
                alreadySelectedValues.Add(cmbRXController.Name, (int)cmbRXController.SelectedValue);
            }

            if (cmbRYController.SelectedIndex != -1)
            {
                alreadySelectedValues.Add(cmbRYController.Name, (int)cmbRYController.SelectedValue);
            }



            return alreadySelectedValues;
        }

        

        private void FreezeControls(bool freeze)
        {
            cmbMIDIDevice.IsEnabled = freeze;
            cmbLXChannel.IsEnabled = freeze;
            cmbLYChannel.IsEnabled = freeze;
            cmbRXChannel.IsEnabled = freeze;
            cmbRYChannel.IsEnabled = freeze;

            cmbLXController.IsEnabled = freeze;
            cmbLYController.IsEnabled = freeze;
            cmbRXController.IsEnabled = freeze;
            cmbRYController.IsEnabled = freeze;
        }

        #endregion

        #region MIDI Methods
        private void SendMIDISignals(SkeletonFrameReadyEventArgs frameArgs)
        {
            SkeletonData skeletonData = (from sd in frameArgs.SkeletonFrame.Skeletons where sd.TrackingState == SkeletonTrackingState.Tracked select sd).FirstOrDefault();

            if (skeletonData == null)
            {
                return;
            }



            if (skeletonData.TrackingState == SkeletonTrackingState.Tracked)
            {
                if (skeletonData.Joints[JointID.HandLeft].TrackingState == JointTrackingState.Tracked &&
                    skeletonData.Joints[JointID.HandRight].TrackingState == JointTrackingState.Tracked)
                {

                    // get the left and right hand Joints


                    Joint jointRight, scaledRight;


                    if (skeletonData.Joints[JointID.HandLeft].TrackingState == JointTrackingState.Tracked)
                    {
                        Joint scaledLeft;
                        Joint jointLeft = skeletonData.Joints[JointID.HandLeft];

                        scaledLeft = jointLeft.ScaleTo((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight, SkeletonMaxX, SkeletonMaxY);


                        byte newValueLX = (byte)(((float)scaledLeft.Position.X / (float)SystemParameters.PrimaryScreenWidth) * 127);
                        byte newValueLY = (byte)(127 - ((float)scaledLeft.Position.Y / (float)SystemParameters.PrimaryScreenHeight) * 127);

                        if (newValueLX != valueLX && ChannelLX != 0 &&ControllerLX != 0)
                        {
                            instrument.OutputChannel = ChannelLX;
                            instrument.SendMessage(MIDIStatusMessages.ControllerChange, ref ControllerLX, valueLX);
                            valueLX = newValueLX;
                            sliderLX.Value = newValueLX;
                        }

                        if (newValueLY != valueLY && ChannelLY != 0 && ControllerLY != 0)
                        {
                            instrument.OutputChannel = ChannelLY;
                            instrument.SendMessage(MIDIStatusMessages.ControllerChange, ref ControllerLY, valueLY);
                            valueLY = newValueLY;
                            sliderLY.Value = newValueLY;
                        }
                    }

                    if (skeletonData.Joints[JointID.HandRight].TrackingState == JointTrackingState.Tracked)
                    {
                        jointRight = skeletonData.Joints[JointID.HandRight];

                        scaledRight = jointRight.ScaleTo((int)SystemParameters.PrimaryScreenWidth, (int)SystemParameters.PrimaryScreenHeight, SkeletonMaxX, SkeletonMaxY);

                        byte newValueRX = (byte)(((float)scaledRight.Position.X / (float)SystemParameters.PrimaryScreenWidth) * 127);
                        byte newValueRY = (byte)(127 - ((float)scaledRight.Position.Y / (float)SystemParameters.PrimaryScreenHeight) * 127);

                        if (newValueRX != valueRX && ChannelRX != 0 && ControllerRX != 0)
                        {
                            instrument.OutputChannel = ChannelRX;
                            instrument.SendMessage(MIDIStatusMessages.ControllerChange, ref ControllerRX, valueRX);
                            valueRX = newValueRX;
                            sliderRX.Value = newValueRX;
                        }

                        if (newValueRY != valueRY && ChannelRY != 0 && ControllerRY != 0)
                        {
                            instrument.OutputChannel = ChannelRY;
                            instrument.SendMessage(MIDIStatusMessages.ControllerChange, ref ControllerRY, valueRY);
                            valueRY = newValueRY;
                            sliderRY.Value = newValueRY;
                        }
                    }
                }
            }
        }

        private void InitialiseMIDIDevice()
        {
            instrument = new Instrument();
            instrument.OutputDeviceName = OutputDeviceName;
            instrument.Open();
        }

        private void CloseMIDIDevice()
        {
            instrument.Close();
        }
        #endregion

        private void cmbLXChannel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
                CheckChannelConflicts(cmbLXChannel, e.AddedItems[0]);
        }

        private void cmbLYChannel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
                CheckChannelConflicts(cmbLYChannel, e.AddedItems[0]);
        }

        private void cmbRXChannel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
                CheckChannelConflicts(cmbRXChannel, e.AddedItems[0]);
        }
        

        private void cmbRYChannel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
                CheckChannelConflicts(cmbRYChannel, e.AddedItems[0]);
        }

        private void cmbLXController_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
                CheckControllerConflicts(cmbLXController, e.AddedItems[0]);
        }

        private void cmbLYController_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
                CheckControllerConflicts(cmbLYController, e.AddedItems[0]);

        }

        private void cmbRXController_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
                CheckControllerConflicts(cmbRXController, e.AddedItems[0]);
        }

        private void cmbRYController_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
                CheckControllerConflicts(cmbRYController, e.AddedItems[0]);
        }

        private void sliderLX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (cmbLXController.SelectedIndex != -1 && cmbLXChannel.SelectedIndex != -1)
            {
                if (sendSignals && valueLX == 0)
                {
                    instrument.OutputChannel = Convert.ToByte(cmbLXChannel.SelectedValue);
                    var controller = Convert.ToByte(cmbLXController.SelectedValue);
                    instrument.SendMessage(MIDIStatusMessages.ControllerChange, ref controller, Convert.ToByte(sliderLX.Value));
                            
                }
            }
        }

        private void sliderLY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (cmbLYController.SelectedIndex != -1 && cmbLYChannel.SelectedIndex != -1)
            {
                if (sendSignals && valueLY == 0)
                {
                    instrument.OutputChannel = Convert.ToByte(cmbLYChannel.SelectedValue);
                    var controller = Convert.ToByte(cmbLYController.SelectedValue);
                    instrument.SendMessage(MIDIStatusMessages.ControllerChange, ref controller, Convert.ToByte(sliderLY.Value));

                }
            }
        }

        private void sliderRX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (cmbRXController.SelectedIndex != -1 && cmbLYChannel.SelectedIndex != -1)
            {
                if (sendSignals && valueRX == 0)
                {
                    instrument.OutputChannel = Convert.ToByte(cmbRXChannel.SelectedValue);
                    var controller = Convert.ToByte(cmbRXController.SelectedValue);
                    instrument.SendMessage(MIDIStatusMessages.ControllerChange, ref controller, Convert.ToByte(sliderRX.Value));

                }
            }
        }

        private void sliderRY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (cmbRYController.SelectedIndex != -1 && cmbRYChannel.SelectedIndex != -1)
            {
                if (sendSignals && valueRY == 0)
                {
                    instrument.OutputChannel = Convert.ToByte(cmbRYChannel.SelectedValue);
                    var controller = Convert.ToByte(cmbRYController.SelectedValue);
                    instrument.SendMessage(MIDIStatusMessages.ControllerChange, ref controller, Convert.ToByte(sliderRY.Value));

                }
            }
        }

        private void btnRefreshMIDIDevices_Click(object sender, RoutedEventArgs e)
        {
            cmbMIDIDevice.ItemsSource = Instrument.OutDeviceNames();
        }

        

        

       

        

        

        
    }
}
