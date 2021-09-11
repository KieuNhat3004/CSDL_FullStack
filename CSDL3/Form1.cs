using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using FireSharp;

namespace CSDL3
{
    public partial class Form1 : Form
    {
        string timeG = "";
        double t1 = 0, h1 = 0;
        bool acx = false;
        string stRoom1, stRoom2, stRoom3,
             stAir, stTV, stFan;
        string ttRoom1, ttRoom2, ttRoom3,
                ttAir, ttTV, ttFan;
        string fRoom1 = "0", fRoom2 = "0", fRoom3 = "0", fTV = "0",
              fAir = "0", fFan = "0";
        string time1a = "", time1b = "", time2a = "", time2b = "",
            txNDT ="", txNDD = "", txDAT = "",txDAD="",
            gNDT ="", gNDD = "", gDAT = "", gDAD ="";
        int StimeLock1 = 0, StimeLock2 = 0;
        int indt =0, indd = 0, idat = 0, idad = 0;

        IFirebaseConfig fcon = new FirebaseConfig()
        {
            AuthSecret = "gRbOC28Gr9uZuEhLV8UAxpEldozFpb5bYvYdDgdi",
            BasePath = "https://winform-esp-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pnlNav.Height = btnDashboard.Height;
            pnlNav.Top = btnDashboard.Top;
            pnlNav.Left = btnDashboard.Left;
            btnDashboard.BackColor = Color.FromArgb(46, 51, 73);

            lbTittle.Text = "Dashboard";
            tabControl1.SelectedIndex = 0;
            
            try
            {
                client = new FireSharp.FirebaseClient(fcon);
                lbCheck.Text = "Connected";
                lbCheck.ForeColor = Color.Green;


            }
            catch
            {
                MessageBox.Show("There was problem in the internet!");
            }
            chart1.Series["Temperature"].Points.Clear();   // Reset, setup lại cột thông tin nhiệt độ trong bảng
            chart1.Series["Humidity"].Points.Clear();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            pnlNav.Height = btnDashboard.Height;
            pnlNav.Top = btnDashboard.Top;
            pnlNav.Left = btnDashboard.Left;
            btnDashboard.BackColor = Color.FromArgb(46, 51, 73);

            btnAna.BackColor = Color.FromArgb(24, 30, 54);
            btnMember.BackColor = Color.FromArgb(24, 30, 54);
            btnTimeSetup.BackColor = Color.FromArgb(24, 30, 54);
            btnAbout.BackColor = Color.FromArgb(24, 30, 54);
            lbTittle.Text = "Dashboard";
        }

        private void btnAna_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            pnlNav.Height = btnAna.Height;
            pnlNav.Top = btnAna.Top;
            btnAna.BackColor = Color.FromArgb(46, 51, 73);

            btnDashboard.BackColor = Color.FromArgb(24, 30, 54);
            btnMember.BackColor = Color.FromArgb(24, 30, 54);
            btnTimeSetup.BackColor = Color.FromArgb(24, 30, 54);
            btnAbout.BackColor = Color.FromArgb(24, 30, 54);

            lbTittle.Text = "Analytics";


        }

        private void btnTimeSetup_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            pnlNav.Height = btnTimeSetup.Height;
            pnlNav.Top = btnTimeSetup.Top;
            btnTimeSetup.BackColor = Color.FromArgb(46, 51, 73);

            btnDashboard.BackColor = Color.FromArgb(24, 30, 54);
            btnMember.BackColor = Color.FromArgb(24, 30, 54);
            btnAbout.BackColor = Color.FromArgb(24, 30, 54);
            btnAna.BackColor = Color.FromArgb(24, 30, 54);

            lbTittle.Text = "Time Setup";
        }

        private void btnMember_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
            pnlNav.Height = btnMember.Height;
            pnlNav.Top = btnMember.Top;
            btnMember.BackColor = Color.FromArgb(46, 51, 73);

            btnDashboard.BackColor = Color.FromArgb(24, 30, 54);
            btnTimeSetup.BackColor = Color.FromArgb(24, 30, 54);
            btnAbout.BackColor = Color.FromArgb(24, 30, 54);
            btnAna.BackColor = Color.FromArgb(24, 30, 54);

            lbTittle.Text = "H&&T Warning";

            textBox1.Text = "NDT : " + gNDT + "\r\n" +
                            "NDD: " + gNDD + "\r\n" +
                            "DAT : " + gDAT + "\r\n" +
                            "DAD: " + gDAD;
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 4;
            pnlNav.Height = btnAbout.Height;
            pnlNav.Top = btnAbout.Top;
            btnAbout.BackColor = Color.FromArgb(46, 51, 73);

            btnDashboard.BackColor = Color.FromArgb(24, 30, 54);
            btnMember.BackColor = Color.FromArgb(24, 30, 54);
            btnTimeSetup.BackColor = Color.FromArgb(24, 30, 54);
            btnAna.BackColor = Color.FromArgb(24, 30, 54);

            lbTittle.Text = "About";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Are you to sure Logout?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                loginout real1 = new loginout();
                real1.Show();
                this.Hide();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Are you to sure close?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeG = lbrTim.Text = DateTime.Now.ToLongTimeString();
        }

        private async void timerReadData_Tick(object sender, EventArgs e)
        {
            IFirebaseClient client = new FirebaseClient(fcon);
            FirebaseResponse response = await client.GetAsync("DataHT_Notime");
            FirebaseResponse response1 = await client.GetAsync("Feedback");
            FirebaseResponse response2 = await client.GetAsync("Control");
            StDevice obj2 = response2.ResultAs<StDevice>();
            Data1 obj = response.ResultAs<Data1>();
            fDevice obj1 = response1.ResultAs<fDevice>();
            t1 = Convert.ToDouble(obj.ND);
            h1 = Convert.ToDouble(obj.DA);

            lbTemp.Text = Convert.ToString(t1);
            lbHumi.Text = Convert.ToString(h1);
            temperature.Value = Convert.ToSByte(t1);
            humidity.Value = Convert.ToSByte(h1);
            fRoom1 = obj1.oRoom1;
            fRoom2 = obj1.oRoom2;
            fRoom3 = obj1.oRoom3;
            fAir = obj1.oAir;
            fTV = obj1.oTV;
            fFan = obj1.oFan;

            ttRoom1 = obj2.btRoom1;
            ttRoom2 = obj2.btRoom2;
            ttRoom3 = obj2.btRoom3;
            ttAir = obj2.btAir;
            ttTV = obj2.btTV;
            ttFan = obj2.btFan;


            ////
            timer1Can.Start();
            if (fRoom1 == "1")
            {
                if (acx == false) { btnRoom1.BackgroundImage = Properties.Resources.swon; stRoom1 = "1"; }
                pRoom1.Image = Properties.Resources.icons8_light_on_40;
                pRoom11.Image = Properties.Resources.icons8_light_on_40;
            }
            else
            {
                if (acx == false) { btnRoom1.BackgroundImage = Properties.Resources.swoff; stRoom1 = "0"; }
                pRoom1.Image = Properties.Resources.icons8_light_off_40;
                pRoom11.Image = Properties.Resources.icons8_light_off_40;
            }
            //////
            if (fRoom2 == "1")
            {
                if (acx == false) { btnRoom2.BackgroundImage = Properties.Resources.swon; stRoom2 = "1"; }
                pRoom2.Image = Properties.Resources.icons8_light_on_40;
                pRoom22.Image = Properties.Resources.icons8_light_on_40;
            }
            else
            {
                if (acx == false) { btnRoom2.BackgroundImage = Properties.Resources.swoff; stRoom2 = "0"; }
                pRoom2.Image = Properties.Resources.icons8_light_off_40;
                pRoom22.Image = Properties.Resources.icons8_light_off_40;
            }
            /////
            if (fRoom3 == "1")
            {
                if (acx == false) { btnRoom3.BackgroundImage = Properties.Resources.swon; stRoom3 = "1"; }
                pRoom3.Image = Properties.Resources.icons8_light_on_40;
                pRoom33.Image = Properties.Resources.icons8_light_on_40;
            }
            else
            {
                if (acx == false) { btnRoom3.BackgroundImage = Properties.Resources.swoff; stRoom3 = "0"; }
                pRoom3.Image = Properties.Resources.icons8_light_off_40;
                pRoom33.Image = Properties.Resources.icons8_light_off_40;
            }

            /////
            if (fAir == "1")
            {
                if (acx == false) { btnAir.BackgroundImage = Properties.Resources.swon; stAir = "1"; }
                pAir.Image = Properties.Resources.turn_on;
                pAir1.Image = Properties.Resources.turn_on;
            }
            else
            {
                if (acx == false) { stAir = "0"; btnAir.BackgroundImage = Properties.Resources.swoff; }
                pAir.Image = Properties.Resources.turn_off;
                pAir1.Image = Properties.Resources.turn_off;
            }

            /////
            if (fTV == "1")
            {
                if (acx == false) { btnTV.BackgroundImage = Properties.Resources.swon; stTV = "1"; }
                pTV.Image = Properties.Resources.television_on;
                pTV1.Image = Properties.Resources.television_on;
            }
            else
            {
                if (acx == false) { btnTV.BackgroundImage = Properties.Resources.swoff; stTV = "0"; }
                pTV.Image = Properties.Resources.turn_off__2_;
                pTV1.Image = Properties.Resources.turn_off__2_;
            }
            /////
            if (fFan == "1")
            {
                if (acx == false)
                {
                    btnFan.BackgroundImage = Properties.Resources.swon;
                    stFan = "1";
                }
                    pFan.Image = Properties.Resources.fan_on;
                    pFan1.Image = Properties.Resources.fan_on;
            }
            else
            {
                if (acx == false) { btnFan.BackgroundImage = Properties.Resources.swoff; stFan = "0"; }
                    pFan.Image = Properties.Resources.fan_off_1_;
                    pFan1.Image = Properties.Resources.fan_off_1_;
            }

            
        }

        private void TloadValueTH_Tick(object sender, EventArgs e)
        {
            chart1.Series["Temperature"].Points.Add(t1); // Gán nhiệt độ vào trong bảng ở cột 2
            chart1.Series["Humidity"].Points.Add(h1);
        }

        private void timer1Can_Tick(object sender, EventArgs e)
        {
            acx = true;
            timer1Can.Stop();
        }

        private void btnRoom1_Click(object sender, EventArgs e)
        {
            if (ttRoom1 == "1") stRoom1 = "0";
            else stRoom1 = "1";

            if (stRoom1 == "1") btnRoom1.BackgroundImage = Properties.Resources.swon;
            else btnRoom1.BackgroundImage = Properties.Resources.swoff;

            StDevice std = new StDevice()
            {
                btRoom1 = stRoom1,
                btRoom2 = stRoom2,
                btRoom3 = stRoom3,
                btAir = stAir,
                btTV = stTV,
                btFan = stFan,
            };
            var settern = client.Set("Control", std);
        }

        private void btnRoom2_Click(object sender, EventArgs e)
        {
            if (ttRoom2 == "1") stRoom2 = "0";
            else stRoom2 = "1";

            if (stRoom2 == "1") btnRoom2.BackgroundImage = Properties.Resources.swon;
            else btnRoom2.BackgroundImage = Properties.Resources.swoff;

            StDevice std = new StDevice()
            {
                btRoom1 = stRoom1,
                btRoom2 = stRoom2,
                btRoom3 = stRoom3,
                btAir = stAir,
                btTV = stTV,
                btFan = stFan,
            };
            var settern = client.Set("Control", std);
        }

        private void btnRoom3_Click(object sender, EventArgs e)
        {
            if (ttRoom3 == "1") stRoom3 = "0";
            else stRoom3 = "1";

            if (stRoom3 == "1") btnRoom3.BackgroundImage = Properties.Resources.swon;
            else btnRoom3.BackgroundImage = Properties.Resources.swoff;

            StDevice std = new StDevice()
            {
                btRoom1 = stRoom1,
                btRoom2 = stRoom2,
                btRoom3 = stRoom3,
                btAir = stAir,
                btTV = stTV,
                btFan = stFan,
            };
            var settern = client.Set("Control", std);
        }

        private void btnAir_Click(object sender, EventArgs e)
        {
            if (ttAir == "1") stAir = "0";
            else stAir = "1";

            if (stAir == "1") btnAir.BackgroundImage = Properties.Resources.swon;
            else btnAir.BackgroundImage = Properties.Resources.swoff;

            StDevice std = new StDevice()
            {
                btRoom1 = stRoom1,
                btRoom2 = stRoom2,
                btRoom3 = stRoom3,
                btAir = stAir,
                btTV = stTV,
                btFan = stFan,
            };
            var settern = client.Set("Control", std);
        }

        private void btnTV_Click(object sender, EventArgs e)
        {
            if (ttTV == "1") stTV = "0";
            else stTV = "1";

            if (stTV == "1") btnTV.BackgroundImage = Properties.Resources.swon;
            else btnTV.BackgroundImage = Properties.Resources.swoff;

            StDevice std = new StDevice()
            {
                btRoom1 = stRoom1,
                btRoom2 = stRoom2,
                btRoom3 = stRoom3,
                btAir = stAir,
                btTV = stTV,
                btFan = stFan,
            };
            var settern = client.Set("Control", std);
        }

        private void btnFan_Click(object sender, EventArgs e)
        {
            if (ttFan == "1") stFan = "0";
            else stFan = "1";

            if (stFan == "1") btnFan.BackgroundImage = Properties.Resources.swon;
            else btnFan.BackgroundImage = Properties.Resources.swoff;

            StDevice std = new StDevice()
            {
                btRoom1 = stRoom1,
                btRoom2 = stRoom2,
                btRoom3 = stRoom3,
                btAir = stAir,
                btTV = stTV,
                btFan = stFan,
            };
            var settern = client.Set("Control", std);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("EntryDate-Time");
            dt.Columns.Add("Temperature");
            dt.Columns.Add("Humidity");

            FirebaseResponse res8 = client.Get(@"Counter");
            {

                int Counter = int.Parse(res8.ResultAs<string>());
                for (int i = 1; i <= Counter; i++)
                {
                    FirebaseResponse res4 = client.Get(@"Stt/" + i + "/Time");
                    string Time = res4.ResultAs<string>();

                    FirebaseResponse res5 = client.Get(@"DataList/" + Time);
                    Data std = res5.ResultAs<Data>();

                    if (std.Time != "")
                    {
                        dt.Rows.Add(std.Time, std.Temperature, std.Humidity);
                    }
                }
                Button btn = (Button)sender;
                DataView dv = new DataView(dt);

                if (btn.Text == "Search")
                {
                    dv.RowFilter = "[" + comboBox1.Text + "]" + "LIKE '%" + searchTbox.Text + "%'";
                }
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = dv;
                dataGridView1.Columns[0].Width = 120;
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Are you to sure delete all data history?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                var result = client.Delete("DataList");
                var result1 = client.Delete("Stt");
                var setdelete = new setDelete
                {
                    DeleteValue = "1",
                };
                FirebaseResponse response = await client.UpdateAsync("StatusDelete", setdelete);
                timerDelete.Start();
            }
        }
        
        private void timerDelete_Tick(object sender, EventArgs e)
        {
            timerDelete.Stop();
            MessageBox.Show("Delete successfully!", "Notify!");
        }

        private void clearWarning_Click(object sender, EventArgs e)
        {
            numricTND.Value = 0;
            numricGND.Value = 0;
            numricTDA.Value = 0;
            numricGDA.Value = 0;
        }

        private async void cbAir_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAir.Checked == true)
            {
                time1a = comboBox21.Text + ":" + comboBox20.Text + ":" + comboBox19.Text;
                time1b = comboBox24.Text + ":" + comboBox23.Text + ":" + comboBox22.Text;
            }
            if (cbAir.Checked == false)
            {
                StimeLock1 = 0;

                var setClock = new setClock
                {
                    timeLock1 = StimeLock1,
                    timeLock2 = StimeLock2,
                };
                FirebaseResponse response8 = await client.UpdateAsync("setClock", setClock);
            }
        }

        private async void cbFan_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFan.Checked == true)
            {
                time2a = comboBox33.Text + ":" + comboBox32.Text + ":" + comboBox31.Text;
                time2b = comboBox36.Text + ":" + comboBox35.Text + ":" + comboBox34.Text;
            }
            if (cbFan.Checked == false)
            {
                StimeLock1 = 0;

                var setClock = new setClock
                {
                    timeLock1 = StimeLock1,
                    timeLock2 = StimeLock2,
                };
                FirebaseResponse response8 = await client.UpdateAsync("setClock", setClock);
            }
        }

        private async void TimerClock_Tick(object sender, EventArgs e)
        {
            if (String.Compare(timeG, time1a, true) == 0)
            {
                StimeLock1 = 1;
                btnAir.Enabled = false;
                var setClock = new setClock
                {
                    timeLock1 = StimeLock1,
                    timeLock2 = StimeLock2,
                };
                FirebaseResponse response8 = await client.UpdateAsync("setClock", setClock);
            }

            if (String.Compare(timeG, time1b, true) == 0)
            {
                StimeLock1 = 0;
                btnAir.Enabled = true;
                var setClock = new setClock
                {
                    timeLock1 = StimeLock1,
                    timeLock2 = StimeLock2,
                };
                FirebaseResponse response8 = await client.UpdateAsync("setClock", setClock);

                stAir = "0";

                StDevice std = new StDevice()
                {
                    btRoom1 = stRoom1,
                    btRoom2 = stRoom2,
                    btRoom3 = stRoom3,
                    btAir = stAir,
                    btTV = stTV,
                    btFan = stFan,
                };
                var settern = client.Set("Control", std);

                cbAir.CheckState = CheckState.Unchecked;
            }



            /////
            ///
            if (String.Compare(timeG, time2a, true) == 0)
            {
                StimeLock2 = 1;
                btnFan.Enabled = false;
                var setClock = new setClock
                {
                    timeLock1 = StimeLock1,
                    timeLock2 = StimeLock2,
                };
                FirebaseResponse response3 = await client.UpdateAsync("setClock", setClock);
            }
            
            if (String.Compare(timeG, time2b, true) == 0)
            {
                StimeLock2 = 0;
                btnFan.Enabled = true;
                var setClock = new setClock
                {
                    timeLock1 = StimeLock1,
                    timeLock2 = StimeLock2,
                };
                FirebaseResponse response3 = await client.UpdateAsync("setClock", setClock);

                stFan = "0";

                StDevice std = new StDevice()
                {
                    btRoom1 = stRoom1,
                    btRoom2 = stRoom2,
                    btRoom3 = stRoom3,
                    btAir = stAir,
                    btTV = stTV,
                    btFan = stFan,
                };
                var settern = client.Set("Control", std);
                cbFan.CheckState = CheckState.Unchecked;
            }
        }

        private async void timerCWarning_Tick(object sender, EventArgs e)
        {
            if (swTND.Checked == true)
            {
                if (t1 > (double)numricTND.Value)
                {
                    indt = 1;
                }
                else
                {
                    indt = 0;
                }
                var std2 = new pointWarning
                {
                    pNDT = Convert.ToString(numricTND.Value),
                    pNDD = Convert.ToString(numricGND.Value),
                    pDAT = Convert.ToString(numricTDA.Value),
                    pDAD = Convert.ToString(numricGDA.Value),
                };
                FirebaseResponse response = await client.UpdateAsync("pointWarning", std2);
            }
            else
            {
                indt = 0;
                var std2 = new pointWarning
                {
                    pNDT = Convert.ToString(numricTND.Value),
                    pNDD = Convert.ToString(numricGND.Value),
                    pDAT = Convert.ToString(numricTDA.Value),
                    pDAD = Convert.ToString(numricGDA.Value),
                };
                FirebaseResponse response = await client.UpdateAsync("pointWarning", std2);
            }
            ////
            if (swGND.Checked == true)
            {
                if (t1 < (double)numricGND.Value)
                {
                    indd = 1;
                }
                else
                {
                    indd = 0;
                }
                var std2 = new pointWarning
                {
                    pNDT = Convert.ToString(numricTND.Value),
                    pNDD = Convert.ToString(numricGND.Value),
                    pDAT = Convert.ToString(numricTDA.Value),
                    pDAD = Convert.ToString(numricGDA.Value),
                };
                FirebaseResponse response = await client.UpdateAsync("pointWarning", std2);
            }
            else
            {
                indd = 0;
                var std2 = new pointWarning
                {
                    pNDT = Convert.ToString(numricTND.Value),
                    pNDD = Convert.ToString(numricGND.Value),
                    pDAT = Convert.ToString(numricTDA.Value),
                    pDAD = Convert.ToString(numricGDA.Value),
                };
                FirebaseResponse response = await client.UpdateAsync("pointWarning", std2);
            }
            //////
            ///
            if (swTDA.Checked == true)
            {
                if (h1 > (double)numricTDA.Value)
                {
                    idat = 1;
                }
                else
                {
                    idat = 0;
                }
                var std2 = new pointWarning
                {
                    pNDT = Convert.ToString(numricTND.Value),
                    pNDD = Convert.ToString(numricGND.Value),
                    pDAT = Convert.ToString(numricTDA.Value),
                    pDAD = Convert.ToString(numricGDA.Value),
                };
                FirebaseResponse response = await client.UpdateAsync("pointWarning", std2);
            }
            else
            {
                idat = 0;
                var std2 = new pointWarning
                {
                    pNDT = Convert.ToString(numricTND.Value),
                    pNDD = Convert.ToString(numricGND.Value),
                    pDAT = Convert.ToString(numricTDA.Value),
                    pDAD = Convert.ToString(numricGDA.Value),
                };
                FirebaseResponse response = await client.UpdateAsync("pointWarning", std2);
            }
            ////
            if (swGDA.Checked == true)
            {
                if (h1 < (double)numricGDA.Value)
                {
                    idad = 1;
                }
                else
                {
                    idad = 0;
                }
                var std2 = new pointWarning
                {
                    pNDT = Convert.ToString(numricTND.Value),
                    pNDD = Convert.ToString(numricGND.Value),
                    pDAT = Convert.ToString(numricTDA.Value),
                    pDAD = Convert.ToString(numricGDA.Value),
                };
                FirebaseResponse response = await client.UpdateAsync("pointWarning", std2);
            }
            else
            {
                idad = 0;
                var std2 = new pointWarning
                {
                    pNDT = Convert.ToString(numricTND.Value),
                    pNDD = Convert.ToString(numricGND.Value),
                    pDAT = Convert.ToString(numricTDA.Value),
                    pDAD = Convert.ToString(numricGDA.Value),
                };
                FirebaseResponse response = await client.UpdateAsync("pointWarning", std2);
            }
        }

        private async void tmxldat_Tick(object sender, EventArgs e)
        {
            if (idat == 1)
            {
                txDAT = "\"" + timeG + "- Humidity is over the threshold!" + "\"";
                var std1 = new Warning
                {
                    GHNDT = txNDT,
                    GHNDD = txNDD,
                    GHDAT = txDAT,
                    GHDAD = txDAD,
                };
                FirebaseResponse response = await client.UpdateAsync("Warning", std1);
            }

            else
            {
                txDAT = "\"No Warning!\"";
                var std1 = new Warning
                {
                    GHNDT = txNDT,
                    GHNDD = txNDD,
                    GHDAT = txDAT,
                    GHDAD = txDAD,
                };
                FirebaseResponse response = await client.UpdateAsync("Warning", std1);
            }
        }

        private async void tmxldad_Tick(object sender, EventArgs e)
        {
            if (idad == 1)
            {
                txDAD = "\"" + timeG + "- Humidity level is lower than threshold!" + "\"";
                var std1 = new Warning
                {
                    GHNDT = txNDT,
                    GHNDD = txNDD,
                    GHDAT = txDAT,
                    GHDAD = txDAD,
                };
                FirebaseResponse response = await client.UpdateAsync("Warning", std1);
            }

            else
            {
                txDAD = "\"No Warning!\"";
                var std1 = new Warning
                {
                    GHNDT = txNDT,
                    GHNDD = txNDD,
                    GHDAT = txDAT,
                    GHDAD = txDAD,
                };
                FirebaseResponse response = await client.UpdateAsync("Warning", std1);
            }
        }

        private async void tmxlndd_Tick(object sender, EventArgs e)
        {
            if (indd == 1)
            {
                txNDD = "\"" + timeG + "- Temperature level is lower than threshold!" + "\"";
                var std1 = new Warning
                {
                    GHNDT = txNDT,
                    GHNDD = txNDD,
                    GHDAT = txDAT,
                    GHDAD = txDAD,
                };
                FirebaseResponse response = await client.UpdateAsync("Warning", std1);
            }

            else
            {
                txNDD = "\"No Warning!\"";
                var std1 = new Warning
                {
                    GHNDT = txNDT,
                    GHNDD = txNDD,
                    GHDAT = txDAT,
                    GHDAD = txDAD,
                };
                FirebaseResponse response = await client.UpdateAsync("Warning", std1);
            }
        }

        private async void tmxlndt_Tick(object sender, EventArgs e)
        {
            if (indt == 1)
            {
                txNDT = "\"" + timeG + "- Temperature is over the threshold!"+ "\"";
                var std1 = new Warning
                {
                    GHNDT = txNDT,
                    GHNDD = txNDD,
                    GHDAT = txDAT,
                    GHDAD = txDAD,
                };
                FirebaseResponse response = await client.UpdateAsync("Warning", std1);
            }

            else
            {
                txNDT = "\"No Warning!\"";
                var std1 = new Warning
                {
                    GHNDT = txNDT,
                    GHNDD = txNDD,
                    GHDAT = txDAT,
                    GHDAD = txDAD,
                };
                FirebaseResponse response = await client.UpdateAsync("Warning", std1);
            }
        }

        private async void timerdelayWarning_Tick(object sender, EventArgs e)
        {
            IFirebaseClient client9 = new FirebaseClient(fcon);
            FirebaseResponse response9 = await client9.GetAsync("Warning");
            Warning obj9 = response9.ResultAs<Warning>();
            gNDT = obj9.GHNDT;
            gNDD = obj9.GHNDD;
            gDAT = obj9.GHDAT;
            gDAD = obj9.GHDAD;
            textBox1.Text = "NDT : " + gNDT + "\r\n" +
                            "NDD: " + gNDD + "\r\n" +
                            "DAT : " + gDAT + "\r\n" +
                            "DAD: " + gDAD;
        }
    }
}
