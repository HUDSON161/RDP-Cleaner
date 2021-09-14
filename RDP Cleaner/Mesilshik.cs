using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RDP_Cleaner
{
    public static class Mesilshik
    {
        public static void Mesi(ref TextBox TB1, ref TextBox TB2)
        {
            string Cl = "";
            TB2.Text = "";
            Form1.NumS = 0;
            int Start;
            int End;
            string Sub;

            for (int i = 0; i < TB1.Lines.Length; i++)
            {
                Cl = TB1.Lines[i].Replace(Environment.NewLine, " ");
                for (int n = 0; n < Cl.Length; n++)
                {
                    if ( n!= 0 && n < Cl.Length - 1 && Cl[n] == ' ' && Cl[n + 1] != ' ')
                    {
                        Start = n + 1;
                        for (int u = n + 1; u < Cl.Length; u++)
                        {
                            if ( u != Cl.Length-1 &&  u < Cl.Length && Cl[u] == ' ')
                            {
                                End = u;
                                Sub = Cl.Substring(Start, End - Start);
                                Cl = Clean(Sub);
                                if (Cl != "")
                                {
                                    if (TB2.Text != "")
                                        TB2.Text = TB2.Text + Environment.NewLine + Cl;
                                    else
                                        TB2.Text = Cl;
                                }
                                break;
                            }
                            if ( u == Cl.Length - 1 )
                            {
                                End = u+1;
                                Sub = Cl.Substring(Start, End - Start);
                                Cl = Clean(Sub);
                                if (Cl != "")
                                {
                                    if (TB2.Text != "")
                                        TB2.Text = TB2.Text + Environment.NewLine + Cl;
                                    else
                                        TB2.Text = Cl;
                                }
                                break;
                            }
                        }
                    }

                    if (n == 0 && Cl[0] != ' ' )
                    {
                        Start = n ;
                        for (int u = n + 1; u < Cl.Length; u++)
                        {
                            if (u != Cl.Length - 1 && u < Cl.Length && Cl[u] == ' ')
                            {
                                End = u;
                                Sub = Cl.Substring(Start, End - Start);
                                Cl = Clean(Sub);
                                if (Cl != "")
                                {
                                    if (TB2.Text != "")
                                        TB2.Text = TB2.Text + Environment.NewLine + Cl;
                                    else
                                        TB2.Text = Cl;
                                }
                                break;
                            }
                            if (u == Cl.Length - 1)
                            {
                                End = u + 1;
                                Sub = Cl.Substring(Start, End - Start);
                                Cl = Clean(Sub);
                                if (Cl != "")
                                {
                                    if (TB2.Text != "")
                                        TB2.Text = TB2.Text + Environment.NewLine + Cl;
                                    else
                                        TB2.Text = Cl;
                                }
                                break;
                            }
                        }
                    }
                }
                Form1.NumS = i;
            }
        }

        public static string Clean(string basicway)//очистка от говна
        {
            string rez = "";

            string basicwayStart = basicway;
            //basicway = Filter.RemoveAfterSpace(basicway);
            //basicway = Filter.RemoveBeforeSpace(basicway);
            int dividers = 0;
            int dividert = 0;
            int[] divider = new int[3];
            int doublepoint = 0;
            int points = 0;

            for (int i = 0; i < basicway.Length; i++)
            {
                dividert = dividers;
                if (basicway[i] == ':' && dividers == 0)
                {
                    doublepoint = i;//нашли двоеточие
                }
                if (doublepoint != 0 && dividert == 0 && ( basicway[i] == '@' || basicway[i] == ';' || basicway[i] == '\\' ) && dividert < 3)
                {
                    divider[dividers] = i;//нашли очередной разделитель
                    dividers++;
                }
                if (doublepoint != 0 && dividert != 0 && (basicway[i] == ';' || basicway[i] == '\\') && dividert < 3)
                {
                    divider[dividers] = i;//нашли очередной разделитель
                    dividers++;
                }
            }
            for (int i = 0; i < doublepoint; i++)
            {
                if (basicway[i] == '.')
                {
                    points++;//нашли точку
                }
            }
            //MessageBox.Show(dividers.ToString());
            //MessageBox.Show("Connected = "+rdp.Connected.ToString());
            if (points == 3 && dividers == 3 && doublepoint != 0 && divider[0] != doublepoint && divider[0] != divider[1] && divider[1] != divider[2] )
            {
                basicway = basicway.Remove(divider[0], 1);
                basicway = basicway.Insert(divider[0],"@");
                basicway = basicway.Remove(divider[1], 1);
                basicway = basicway.Insert(divider[1], @"\");
                basicway = basicway.Remove(divider[2], 1);
                basicway = basicway.Insert(divider[2], ";");
                rez = basicway;
            }
            if (points == 3 && dividers == 2 && doublepoint != 0 && divider[0] != doublepoint && divider[0] != divider[1] )
            {
                basicway = basicway.Remove(divider[0], 1);
                basicway = basicway.Insert(divider[0], "@");
                basicway = basicway.Remove(divider[1], 1);
                basicway = basicway.Insert(divider[1], ";");
                rez = basicway;
            }
            if (points == 3 && dividers == 1 && doublepoint != 0 && divider[0] != doublepoint )
            {
                basicway = basicway.Remove(divider[0], 1);
                basicway = basicway.Insert(divider[0], "@");
                rez = basicway;
            }
            if (points == 3 && dividers == 0 && doublepoint != 0 && doublepoint != basicway.Length - 1)
            {
                //rez = basicway;
            }

            return rez;
        }

        public static void DestroyTheSame(ref TextBox TB1, ref TextBox TB2)
        {
            Form1.NumS = 0;

            for (int i = 0; i < TB1.Lines.Length; i++)
            {
                if ( TB2.Lines.Length  <=0 && TB1.Lines[i] != "")
                {
                    TB2.Text += TB1.Lines[i];
                }
                if (TB2.Lines.Length > 0 && TB1.Lines[i] != "")
                {
                    for (int j = 0; j < TB2.Lines.Length; j++)
                    {
                        if (TB2.Lines[j].Contains(TB1.Lines[i]))
                        {
                            break;
                        }
                        if (j == TB2.Lines.Length - 1 && !TB2.Lines[j].Contains(TB1.Lines[i]))
                        {
                            TB2.Text += Environment.NewLine + TB1.Lines[i];
                            break;
                        }
                    }
                }
                Form1.NumS = i;
            }
        }

        public static void MesiSMTP(ref TextBox TB1, ref TextBox TB2,ref TextBox TB3,bool bCheck)
        {
            string Cl = "";
            TB2.Text = "";
            Form1.NumS = 0;
            string Sub;

            string TMail = "";
            string TServer = "";
            string TPort = "";
            string TIp = "";
            string TUsername = "";
            string TPassword = "";

            bool bMail = false;
            bool bServer = false;
            bool bPort = false;
            bool bIp = false;
            bool bUsername = false;
            bool bPassword = false;

            for (int i = 0; i < TB1.Lines.Length; i++)
            {
                TMail = "";
                TServer = "";
                TPort = "";
                TIp = "";
                TUsername = "";
                TPassword = "";

                bMail = false;
                bServer = false;
                bPort = false;
                bIp = false;
                bUsername = false;
                bPassword = false;

                Cl = TB1.Lines[i].Replace(Environment.NewLine, " ");
                if ( !bCheck )
                {
                    FindX(ref bMail, ref Cl, ref TMail, "EMail:");
                    FindX(ref bServer, ref Cl, ref TServer, "SMTP Server:");
                    FindX(ref bIp, ref Cl, ref TIp, "IP:");
                    FindX(ref bPort, ref Cl, ref TPort, "Port:");
                    FindX(ref bUsername, ref Cl, ref TUsername, "Username:");
                    FindX(ref bPassword, ref Cl, ref TPassword, "Password:");
                }
                else
                {
                    int NumG = 0;
                    int IndG = 0;
                    bool bUsed;
                    for (int g = 0; g < Cl.Length; g++)
                    {
                        bUsed = false;
                        if ( !bUsed && Cl[g] == ',' && NumG == 0)
                        {
                            if (IndG < Cl.Length && g - IndG <= Cl.Length - IndG && g - IndG > 0)
                            {
                                TServer = Cl.Substring(IndG, g - IndG);
                                //MessageBox.Show(TServer);
                            }
                            NumG++;
                            IndG = g;
                            bUsed = true;
                        }
                        if ( !bUsed && Cl[g] == ',' && NumG == 1)
                        {
                            if ( IndG + 1  < Cl.Length && g - (IndG + 1) <= Cl.Length - (IndG + 1) && g - (IndG + 1) > 0 )
                            {
                                TPort = Cl.Substring(IndG + 1, g - (IndG + 1));
                                //MessageBox.Show(TPort);
                            }
                            NumG++;
                            IndG = g;
                            bUsed = true;
                        }
                        if ( !bUsed && Cl[g] == ',' && NumG == 2)
                        {
                            NumG++;
                            IndG = g;
                            bUsed = true;
                        }
                        if ( !bUsed && Cl[g] == ',' && NumG == 3)
                        {
                            if (IndG + 1 < Cl.Length && g - (IndG + 1) <= Cl.Length - (IndG + 1) && g - (IndG + 1) > 0 )
                            {
                                TUsername = Cl.Substring(IndG + 1, g - (IndG + 1));
                                //MessageBox.Show(TUsername);
                            }
                            NumG++;
                            IndG = g;
                            bUsed = true;
                        }
                        if ( !bUsed && NumG == 4 && g == Cl.Length -1)
                        {
                            if (IndG + 1 < Cl.Length && Cl.Length - (IndG + 1) <= Cl.Length - (IndG + 1) && Cl.Length - (IndG + 1) > 0)
                            {
                                TPassword = Cl.Substring(IndG + 1, Cl.Length - (IndG + 1));
                                //MessageBox.Show(TPassword);
                            }
                            NumG++;
                            IndG = g;
                            bUsed = true;
                            break;
                        }
                    }
                }

                if ( bCheck )
                {
                    if (Cl != "" && ValidateSMTP(ref TB1, ref TB2, ref TB3, ref TServer, ref TPort, ref TUsername) )
                    {
                        if (TB2.Text != "")
                        {
                            Sub = Environment.NewLine;
                            Sub += TServer + "," + TPort + "," + "1" + "," + TUsername + "," + TPassword;
                            TB2.Text += Sub;
                        }
                        else
                        {
                            Sub = "Server Name,Port,Authentication,User Name,Password" + Environment.NewLine;
                            Sub += TServer + "," + TPort + "," + "1" + "," + TUsername + "," + TPassword;
                            TB2.Text = Sub;
                        }
                    }
                }
                else
                {
                    if (Cl != "")
                    {
                        if (TB2.Text != "")
                        {
                            Sub = Environment.NewLine;
                            Sub += TServer + "," + TPort + "," + "1" + "," + TUsername + "," + TPassword;
                            TB2.Text += Sub;
                        }
                        else
                        {
                            Sub = "Server Name,Port,Authentication,User Name,Password" + Environment.NewLine;
                            Sub += TServer + "," + TPort + "," + "1" + "," + TUsername + "," + TPassword;
                            TB2.Text = Sub;
                        }
                    }
                }

                Form1.NumS = i;
            }
        }

        private static void FindX(ref bool b1,ref string Cl, ref string Rez,string SearchStr)
        {
            for (int i = 0; i < Cl.Length; i++)
            {
                if (!b1 && i < Cl.Length - SearchStr.Length && Cl.Substring(i, SearchStr.Length) == SearchStr)
                {
                    //MessageBox.Show(Cl.Substring(i, SearchStr.Length));
                    for (int j = i + SearchStr.Length; j < Cl.Length; j++)
                    {
                        if (Cl[j] == ' ' || Cl[j] == '	' )
                        {
                            Rez = Cl.Substring(i + SearchStr.Length, j - (i + SearchStr.Length));
                            b1 = true;
                            //MessageBox.Show(Rez);
                            break;
                        }
                    }
                    if (!b1)
                    {
                        Rez = Cl.Substring(i + SearchStr.Length, Cl.Length - (i + SearchStr.Length));
                        b1 = true;
                        //MessageBox.Show(Rez);
                    }
                    break;
                }
            }
        }

        private static void FindY(ref bool b1, ref string Cl, ref string Rez, string SearchStr)
        {
            for (int i = 0; i < Cl.Length; i++)
            {
                if (!b1 && i < Cl.Length - SearchStr.Length && Cl.Substring(i, SearchStr.Length) == SearchStr)
                {
                    Rez = Cl.Substring(i + SearchStr.Length, Cl.Length - (i + SearchStr.Length));
                    b1 = true;
                    break;
                }
            }
        }

        private static int FindEmptyIndex(int index0,ref TextBox TB3)
        {
            int rez = -1;
            for (int i = index0; i >= 0; i--)
            {
                if ( TB3.Lines[i] == "" )
                {
                    rez=i;
                    break;
                }
                if ( i == 0)
                {
                    rez = 0;
                    break;
                }
            }
            return rez;
        }

        private static bool ValidateSMTP(ref TextBox TB1, ref TextBox TB2, ref TextBox TB3,ref string TTServer, ref string TTPort, ref string TTUsername)
        {
            bool rez = false;
            string Cl = "";

            string TServer = "";
            string TPort = "";
            string TUsername = "";

            bool bServer = false;
            bool bPort = false;
            bool bUsername = false;

            int IndexS;

            for (int i = 0; i < TB3.Lines.Length; i++)
            {
                TServer = "";
                TPort = "";
                string ts = "";
                TUsername = "";

                bServer = false;
                bPort = false;
                bUsername = false;

                //MessageBox.Show(TB3.Lines[i]);
                if ( TB3.Lines[i] == "Result: OK" )
                {
                    IndexS = FindEmptyIndex(i, ref TB3);
                    //MessageBox.Show("ereb");
                    if ( IndexS != -1)
                    {
                        for (int j = IndexS; j < i; j++)
                        {
                            Cl = TB3.Lines[j].Replace(Environment.NewLine, " ");
                            FindY(ref bServer, ref Cl, ref TServer, "Server: ");
                            FindY(ref bPort, ref Cl, ref TPort, "IP (port): ");
                            FindY(ref bUsername, ref Cl, ref TUsername, "User: ");
                            for (int h = TPort.Length - 1 ; h >= 0; h--)
                            {
                                if ( TPort[h] == '(' )
                                {
                                    TPort = TPort.Substring(h + 1, TPort.Length-(h + 2));
                                    //MessageBox.Show(TPort);
                                    break;
                                }
                            }
                            //MessageBox.Show(TServer);
                            //MessageBox.Show(TPort);
                            //MessageBox.Show(TUsername);
                        }

                        if (TServer == TTServer && TPort == TTPort && TUsername == TTUsername)
                        {
                            rez = true;
                            break;
                        }
                    }
                }
            }
            return rez;
        }
    }
}
