using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MIDIWrapper;

namespace MIDIWrapperTest
{
    public partial class MIDIWrapperTest : Form
    {

        private List<string> outDevices = new List<string>(Instrument.OutDeviceNames());
        Instrument instrument = new Instrument();
        public MIDIWrapperTest()
        {
            InitializeComponent();
        }

        private void MIDIWrapperTest_Load(object sender, EventArgs e)
        {
            cmbTranspose.SelectedIndex = 12;

            cmbDevices.DataSource = outDevices;
            if (outDevices.Count > 0)
            {
                cmbDevices.SelectedIndex = 0;
            }

            cmbGM.DataSource = Instrument.GMInstrumentNames;
            cmbGM.SelectedIndex = 0;

            pb0.MouseDown += new MouseEventHandler(NoteOnHandler);
            pb1.MouseDown += new MouseEventHandler(NoteOnHandler);
            pb2.MouseDown += new MouseEventHandler(NoteOnHandler);
            pb3.MouseDown += new MouseEventHandler(NoteOnHandler);
            pb4.MouseDown += new MouseEventHandler(NoteOnHandler);
            pb5.MouseDown += new MouseEventHandler(NoteOnHandler);
            pb6.MouseDown += new MouseEventHandler(NoteOnHandler);
            pb7.MouseDown += new MouseEventHandler(NoteOnHandler);
            pb8.MouseDown += new MouseEventHandler(NoteOnHandler);
            pb9.MouseDown += new MouseEventHandler(NoteOnHandler);
            pb10.MouseDown += new MouseEventHandler(NoteOnHandler);
            pb11.MouseDown += new MouseEventHandler(NoteOnHandler);
            pb12.MouseDown += new MouseEventHandler(NoteOnHandler);

            pb0.MouseUp += new MouseEventHandler(NoteOffHandler);
            pb1.MouseUp += new MouseEventHandler(NoteOffHandler);
            pb2.MouseUp += new MouseEventHandler(NoteOffHandler);
            pb3.MouseUp += new MouseEventHandler(NoteOffHandler);
            pb4.MouseUp += new MouseEventHandler(NoteOffHandler);
            pb5.MouseUp += new MouseEventHandler(NoteOffHandler);
            pb6.MouseUp += new MouseEventHandler(NoteOffHandler);
            pb7.MouseUp += new MouseEventHandler(NoteOffHandler);
            pb8.MouseUp += new MouseEventHandler(NoteOffHandler);
            pb9.MouseUp += new MouseEventHandler(NoteOffHandler);
            pb10.MouseUp += new MouseEventHandler(NoteOffHandler);
            pb11.MouseUp += new MouseEventHandler(NoteOffHandler);
            pb12.MouseUp += new MouseEventHandler(NoteOffHandler);

        }

        private void NoteOnHandler(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            byte note = Convert.ToByte(Convert.ToByte(pb.Name.Substring(2)) + 60);
            byte velocity = 53;
            if (instrument.Engaged == false)
            {
                MessageBox.Show("Please open the device first");
            }
            else
            {
                instrument.PlayNote( note, velocity);
            }
        }

        private void NoteOffHandler(object sender, MouseEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            byte note = Convert.ToByte(pb.Name.Substring(2));
            if (instrument.Engaged == false)
            {
                MessageBox.Show("Please open the device first");
            }
            else
            {
                instrument.StopNote(note);
            }
        }

        private void cmbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbGM_SelectedIndexChanged(object sender, EventArgs e)
        {
            instrument.ChangePatchGM(cmbGM.SelectedValue.ToString());
        }

        private void txtNoteDuration_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int duration = Convert.ToInt32(txtNoteDuration.Text);
                instrument.NoteDuration = duration;
            }
            catch
            {
            }
        }

        private void cmbTranspose_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTranspose.Text != String.Empty)
            {
                switch (cmbTranspose.Text.ToLowerInvariant())
                {
                    case "+1 octave":
                        {
                            instrument.Transpose = 12;
                            break;
                        }

                    case "-1 octave":
                        {
                            instrument.Transpose = -12;
                            break;
                        }

                    default:
                        {
                            try
                            {
                                instrument.Transpose = Convert.ToInt32(cmbTranspose.Text);
                            }
                            catch
                            {
                            }
                            break;
                        }

                }
            }
        }

        private void cmbTranspose_TextChanged(object sender, EventArgs e)
        {
            cmbTranspose_SelectedIndexChanged(sender, e);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (instrument.Engaged == false)
            {
                instrument.OutputDeviceName = outDevices[cmbDevices.SelectedIndex];
                instrument.OutputChannel = 0;
                instrument.Open();
                instrument.PatchNumber = 0;
                instrument.Volume = 127;
                btnOpen.Text = "Close";
                cmbDevices.Enabled = false;
            }
            else
            {
                instrument.Close();
                cmbDevices.Enabled = true;
                btnOpen.Text = "Open";
            }
        }

        private void vsVolume_Scroll(object sender, ScrollEventArgs e)
        {
            instrument.Volume = Convert.ToByte(127 - vsVolume.Value);
        }
    }
}
