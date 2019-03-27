using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.IO;

namespace BBSGenerator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int pf = 0;
        int pff = 0;
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }
        public String Gen = null;
        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }

        public static int GetGCDByModulus(int value1, int value2)
        {
            while (value1 != 0 && value2 != 0)
            {
                if (value1 > value2)
                    value1 %= value2;
                else
                    value2 %= value1;
            }
            return Math.Max(value1, value2);
        }

        public static bool Coprime(int value1, int value2)
        {
            return GetGCDByModulus(value1, value2) == 1;
        }

        public MainWindow()
        {
            InitializeComponent();
            //  comboBox.Items.Add(13);
            for (int i = 0; i <= 100000; i++) {
                if (((i - 3) % 4 == 0) && (IsPrime(i) == true)) {
                    comboBox.Items.Add(i);
                    comboBox2.Items.Add(i);
                };

            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            int s;
            if (!(comboBox.Text == "" || comboBox2.Text == "" || N.Text == ""))
            {
                if (IsPrime(int.Parse(comboBox.Text)))
                {
                    if (IsPrime(int.Parse(comboBox2.Text)))
                    {
                        Random rnd = new Random();

                        bool locket = false;
                        if (S.Text == "")
                        {
                            while (locket == false)
                            {
                                S.Text = rnd.Next(2, 100000).ToString();
                                if (Coprime(int.Parse(S.Text), int.Parse(comboBox.Text) * int.Parse(comboBox2.Text)) == true) { s = int.Parse(S.Text); locket = true; }

                            }
                            Console.WriteLine("znaleziono s ");
                        }
                        else { MessageBoxResult noIter = MessageBox.Show("S may not be Coprime with p*q", "Alert!"); };
                        s = int.Parse(S.Text);
                        int p = int.Parse(comboBox.Text);
                        int q = int.Parse(comboBox2.Text);
                        int n = int.Parse(N.Text);
                        int m = q * p;

                        //string Gen = null;
                        int a = 0;
                        a = (s * s) % m;
                        if (a % 2 == 0)
                            Gen += "0";
                        // if (a % 2 == 1)
                        else
                        {
                            Gen += "1";
                        }


                        for (int i = 0; i < (n - 1); i++)
                        {
                            int b = (a * a) % m;
                            a = b;
                            if (b % 2 == 0)
                                Gen += "0";
                            //if (b % 2 == 1)
                            else
                            {
                                Gen += "1";
                            }
                        //    Console.WriteLine(Gen.Count());
                        }

                        Key.Text = Gen;
                    }
                    else
                    {
                        MessageBoxResult noIter = MessageBox.Show("p is not a prime number!", "Alert!");
                    }
                }
                else
                {
                    MessageBoxResult noIter = MessageBox.Show("q is not a prime number!", "Alert!");
                }
            }
            else
            {
                MessageBoxResult noIter = MessageBox.Show("All Boxes should be filled.", "Alert!");
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Key.Text = null;
            S.Text = null;
            comboBox2.Text = null;
            comboBox.Text = null;
            N.Text = null;
        }

        private void S_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (S.Text == "0")
            {
                S.Text = "1";
            }
        }





        private void N_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (N.Text == "0")
            {
                N.Text = "1";
            }
        }

        private void eSubmit_Click(object sender, RoutedEventArgs e)
        {
            int s;
            if (!(eQ.Text == "" || eP.Text == "" || eN.Text == ""))
            {
                if (IsPrime(int.Parse(eQ.Text)))
                {
                    if (IsPrime(int.Parse(eP.Text)))
                    {
                        Random rnd = new Random();

                        bool locket = false;
                        if (eS.Text == "")
                        {
                            while (locket == false)
                            {
                                eS.Text = rnd.Next(2, 1000).ToString();
                                if (Coprime(int.Parse(eS.Text), int.Parse(eP.Text) * int.Parse(eQ.Text)) == true) { s = int.Parse(eS.Text); locket = true; }

                            }
                        }
                        else { MessageBoxResult noIter = MessageBox.Show("S may not be Coprime with p*q", "Alert!"); };
                        s = int.Parse(eS.Text);
                        int p = int.Parse(eP.Text);
                        int q = int.Parse(eQ.Text);
                        int n = int.Parse(eN.Text);
                        int m = q * p;
                        M.Text = ("M = " + m.ToString());

                        string Gen = null;
                        int a = 0;
                        a = (s * s) % m;
                        if (a % 2 == 0)
                        {
                            Gen += "0";
                            B.Text = "0";
                        }
                        if (a % 2 == 1)
                        {
                            Gen += "1";
                            B.Text = "1";
                        }
                        X.Text = ("x1 = " + a.ToString());


                        for (int i = 0; i < (n - 1); i++)
                        {
                            int b = (a * a) % m;
                            a = b;
                            if (b % 2 == 0)
                                Gen += "0";
                            if (b % 2 == 1)
                                Gen += "1";
                        }

                        eKey.Text = Gen;
                    }
                    else
                    {
                        MessageBoxResult noIter = MessageBox.Show("p is not a prime number!", "Alert!");
                    }
                }
                else
                {
                    MessageBoxResult noIter = MessageBox.Show("q is not a prime number!", "Alert!");
                }
            }
            else
            {
                MessageBoxResult noIter = MessageBox.Show("All Boxes should be filled.", "Alert!");
            }
        }

        private void eReset_Click(object sender, RoutedEventArgs e)
        {
            eN.Text = null;
            eQ.Text = null;
            eP.Text = null;
            eS.Text = null;
            B.Text = null;
            M.Text = null;
            X.Text = null;
            eKey.Text = null;
        }

        private void eN_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (eN.Text == "0")
            {
                eN.Text = "1";
            }
        }

        private void eS_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (eS.Text == "0")
            {
                eS.Text = "1";
            }
        }

        private void eP_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (eP.Text == "0")
            {
                eP.Text = "1";
            }
        }

        private void eQ_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (eQ.Text == "0")
            {
                eQ.Text = "1";
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            Console.WriteLine(Key.Text.Count());
            string write = Key.Text;
            System.IO.File.WriteAllText("d:/szyfr/" + "Generator" + pf + ".txt", write, Encoding.GetEncoding("Windows-1250"));
            string write2 = "Generator BBS" + "\r\n" + "s= " + S.Text + "\r\n" + "q= " + comboBox2.Text + "\r\n" + "p= " + comboBox.Text + "\r\n" + "Dlugosc= " + N.Text;
            System.IO.File.WriteAllText("d:/szyfr/" + "Dane" + pf + ".txt", "Michał Maciaszek\r\n" + write2, Encoding.GetEncoding("Windows-1250"));
            pf++;
        }

        //dla strumieniowego
        static byte[] EncodeToBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        static string DecodeToString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
        public Byte[] GetBytesFromBinaryString(String binary)
        {
            var list = new List<Byte>();

            for (int i = 0; i < binary.Length; i += 8)
            {
                String t = binary.Substring(i, 8);

                list.Add(Convert.ToByte(t, 2));
            }

            return list.ToArray();
        }

        public static string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }

        byte[] byt;
        string encoded = null;
        string decoded = null;
        List<char> v = new List<char>();

        private void Cipher_Click(object sender, RoutedEventArgs e)
        {
            if (!(Message.Text.Length == 0))
            {
                if (!(generator.Text == ""))
                    if (generator.Text.Count() > Message.Text.Count() * 16)
                    {
                        {
                            byte[] by = EncodeToBytes(Message.Text);

                            string[] s = by.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')).ToArray();
                            string ss = "";
                            for (int i = 0; i < s.Length; i++)
                            {

                                ss += s[i];
                            }

                            encoded = null;
                            for (int i = 0; i < Message.Text.Length * 16; i++)
                            {
                                if (ss[i] == generator.Text[i]) encoded += "0";
                                else encoded += "1";
                            }

                            EncodedMessage.Text = encoded;
                        }
                    }
                    else { MessageBoxResult noIter = MessageBox.Show("Za krotki klucz", "Alert!"); }
                else
                {
                    MessageBoxResult noIter = MessageBox.Show("You need to create the key first!", "Alert!");
                }
            }
            else
            {
                MessageBoxResult noIter = MessageBox.Show("You need to enter the message first!", "Alert!");
            }
        }
        int byteIndex;
        int bitIndex;
        int numBytes;

        private byte[] toByteArray(String text)
        {
            numBytes = text.Length / 8;

            byt = new byte[numBytes];
            byteIndex = 0;
            bitIndex = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '1')
                {
                    byt[byteIndex] |= (byte)(1 << (7 - bitIndex));
                }

                bitIndex++;

                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    byteIndex++;
                }
            }
            return byt;
        }

        private void Decipher_Click(object sender, RoutedEventArgs e)
        {
            //  if (!(Message.Text.Length == 0))
            //{
            if (!(generator.Text == ""))
            {
                if (!(EncodedMessage.Text.Length == 0))
                {
                    decoded = null;
                    encoded = EncodedMessage.Text;
                    for (int i = 0; i < EncodedMessage.Text.Length; i++)
                    {

                        if (encoded[i] == '1' && generator.Text[i] == '1') decoded += "0";
                        if (encoded[i] == '0' && generator.Text[i] == '0') decoded += "0";
                        if (encoded[i] == '1' && generator.Text[i] == '0') decoded += "1";
                        if (encoded[i] == '0' && generator.Text[i] == '1') decoded += "1";
                    }
                    DecodedMessage.Text = decoded;
                    //   DecodedMessage2.Text = BinaryToString(DecodedMessage.Text);
                    byt = toByteArray(decoded);
                    string cdecoded = DecodeToString(byt);
                    DecodedMessage2.Text = cdecoded;

                }
                else
                {
                    MessageBoxResult noIter = MessageBox.Show("You need to cipher the message first!", "Alert!");
                }
            }
            else
            {
                MessageBoxResult noIter = MessageBox.Show("You need to create the key first!", "Alert!");
            }
            //}
            //else
            //{
            //  MessageBoxResult noIter = MessageBox.Show("You need to enter the message first!", "Alert!");
            // }
        }

        private void generator_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Message_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DecodedMessage_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void EncodedMessage_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Openkey_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                string path = System.IO.Path.GetDirectoryName(filename);
                //string uploadPath = Server.MapPath("~/uploads");
                string contents = File.ReadAllText(@filename);
                generator.Text = contents;
            }
        }

        private void Opentext_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                string path = System.IO.Path.GetDirectoryName(filename);
                //string uploadPath = Server.MapPath("~/uploads");
                string contents = File.ReadAllText(@filename, Encoding.GetEncoding("Windows-1250"));
                Message.Text = contents;
            }
        }


        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //   List<int> data = new List<int>();
            // data.Add(13);
            // ..data.Add(23);

            // var combo = sender as ComboBox;
            // combo.ItemsSource = data;
            // combo.SelectedIndex = 0;
        }

        private void comboBox_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void comboBox2_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DecodedMessage2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Wgraj_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                string path = System.IO.Path.GetDirectoryName(filename);
                //string uploadPath = Server.MapPath("~/uploads");
                string contents = File.ReadAllText(@filename, Encoding.GetEncoding("Windows-1250"));
                EncodedMessage.Text = contents;
            }
        }

        private void Zapisz1_Click(object sender, RoutedEventArgs e)
        {
            string write = EncodedMessage.Text;
            System.IO.File.WriteAllText("d:/szyfr/" + "Zestrumieniowane" + pff + ".txt", write, Encoding.GetEncoding("Windows-1250"));
            pff++;

        }
  
        private string dbf_File;

        private void Openkey2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                string path = System.IO.Path.GetDirectoryName(filename);
                //string uploadPath = Server.MapPath("~/uploads");
                string contents = File.ReadAllText(@filename);
                Tekst.Text = contents;
             
                dbf_File = System.IO.Path.GetFileName(filename);
                //    Console.WriteLine("ściezka" + dbf_File);
                if (Tekst.Text.Count() > 20000) { MessageBoxResult noIter = MessageBox.Show("Wiecej niz 20000 bitów", "Alert!");
                    
                        string s = Tekst.Text;
                        string sub = s.Substring(0, 20000);
                        Tekst.Text = sub;
                    }

                if (Tekst.Text.Count() < 20000)
                {
                    MessageBoxResult noIter2 = MessageBox.Show("Mniej niz 20000 bitów", "Alert!");
                    //while (Tekst.Text.Count() < 20000)
                    //{
                    //    Tekst.Text += "0";
                    //    Console.WriteLine(Tekst.Text.Count());
                    //}


                }


            }
        }

        private void Tekst_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        int licz = 0;
        private void Bittest_Click(object sender, RoutedEventArgs e)
        {
            licz = 0;
            string s = Tekst.Text;

            for (int i = 0; i < s.Count(); i++)
            {
                if (s[i] == '1') licz++;
                //            else { Console.WriteLine("brak"); }
            }

            if (licz > 9725 && licz < 10275) { wynikiBit.Text = "zdane"; }
            else { wynikiBit.Text = "niezdane"; }

        }

        private void wynikiBit_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        int a1 = 0; int a3 = 0; int a2 = 0; int a4 = 0; int a5 = 0;
        int a6 = 0;
        int b1, b2, b3, b4, b5, b6;
        private void Seriestest_Click(object sender, RoutedEventArgs e)
        {
            a1 = 0; a2 = 0; a3 = 0; a4 = 0; a5 = 0; a6 = 0;
            b1 = 0;b2 = 0;b3 = 0;b4 = 0; b5 = 0; b6 = 0;
           // int a61 = 0; int a51 = 0; int a41 = 0; int a21 = 0; int a31 = 0; int a11 = 0;
            int previous = Tekst.Text[0];
            int count =0;
            int count1 = 0;
            for (int i=1; i<Tekst.Text.Count();i++)
            {
                if (Tekst.Text[i] == '0') {
                    if (Tekst.Text[i] == '0' && previous == 0) { count++; }

                    else
                    {
                        if (count == 0) a1++;
                        if (count == 1) a2++;
                        if (count == 2) a3++;
                        if (count == 3) a4++;
                        if (count == 4) a5++;
                        if (count >= 5) a6++;
                        count = 0;
                    }
                }
                if (Tekst.Text[i] == '1') {
                    if (Tekst.Text[i] == '1' && previous == 1) { count1++; }
                    else
                    {
                        if (count1 == 0) b1++;
                        if (count1 == 1) b2++;
                        if (count1 == 2) b3++;
                        if (count1 == 3) b4++;
                        if (count1 == 4) b5++;
                        if (count1 >= 5) b6++;
                        count1 = 0;
                    }         

            }
                if (Tekst.Text[i] == '0') previous = 0;
                if (Tekst.Text[i] == '1') previous = 1;





            }


             //   Console.WriteLine(a1);
               // Console.WriteLine(a2);
               // Console.WriteLine(a3);
               // Console.WriteLine(a4);
                //Console.WriteLine(a5);
               // Console.WriteLine(a6);

            if (a1 >= 2315 && a1 <= 2685  && a2 >= 1114 && a2 <= 1386 && a3 >= 527 && a3 <= 723 && a4 >= 240 && a4 <= 384 && a5 >= 103 && a5 <= 209 && a6 >= 103 && a6 <= 209 && b1 >= 2315 && b1 <= 2685 && b2 >= 1114 && b2 <= 1386 && b3 >= 527 && b3 <= 723 && b4 >= 240 && b4 <= 384 && b5 >= 103 && b5 <= 209 && b6 >= 103 && b6 <= 209) { WynikiSeries.Text = "zdane"; } else { WynikiSeries.Text = "niezdane"; }



        }

        private void WynikiSeries_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        int count = 0;
        //private void TestLong_Click(object sender, RoutedEventArgs e)
        //{
        //    int a = 0;
        //    int previous = 2;


        //    for (int i = 0; i < Tekst.Text.Count(); i++)
        //    {
        //        if ((Tekst.Text[i] == '1' && previous == 1) || (Tekst.Text[i] == '0' && previous == 0)) { count++; }

        //        else
        //        {

        //            if (count >= 26)
        //            {
        //                a++;
        //                break;
        //            }

        //                count = 0;

        //        }

        //        if (Tekst.Text[i] == '0') previous = 0;
        //        if (Tekst.Text[i] == '1') previous = 1;

        //    }

        //    if (a > 0) { WynikiLong.Text = "niezdane"; return; } else { WynikiLong.Text = "zdane"; }
        //}
        int max = 0;
        private void TestLong_Click(object sender, RoutedEventArgs e)
        {
            max = 0;
            if (Tekst.Text.Length == 20000)
            {
                int count2 = 1;
                int previous2 = 2;
                bool b3 = true;
                for (int i = 0; i < 20000; i++)
                {
                    if (count2 == 25)
                    {
                        b3 = false;
                        break;
                    }
                    if (Tekst.Text[i] == '1' && previous2 == 1) count2++;
                    if (Tekst.Text[i] == '0' && previous2 == 0) count2++;
                    else
                    {
                        if (max < count2)
                            max = count2;
                        count2 = 1;
                    }
                    if (Tekst.Text[i] == '1') previous2 = 1;
                    if (Tekst.Text[i] == '0') previous2 = 0;
                }

                if (b3)
                    WynikiLong.Text = "zdane";
                else
                    WynikiLong.Text = "niezdane";
            }
            else { WynikiLong.Text = "Tekst był krotszy niz 20000"; }
        }


        private void WynikiLong_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        double wynik = 0;
        private void TestPoker_Click(object sender, RoutedEventArgs e)
        {
            wynik = 0;
                int sum1 = 0;
                int sum2 = 0;
                int sum3 = 0;
                int sum4 = 0;
                int sum5 = 0;
                int sum6 = 0;
                int sum7 = 0;
                int sum8 = 0;
                int sum9 = 0;
                int sum10 = 0;
                int sum11 = 0;
                int sum12 = 0;
                int sum13 = 0;
                int sum14 = 0;
                int sum15 = 0;
                int sum16 = 0;


                for (int i = 0; i < Tekst.Text.Count() - 3; i = i + 4)
                {
                    if (Tekst.Text[i] == '0' && Tekst.Text[i + 1] == '0' && Tekst.Text[i + 2] == '0' && Tekst.Text[i + 3] == '0') sum1++;

                    if (Tekst.Text[i] == '0' && Tekst.Text[i + 1] == '0' && Tekst.Text[i + 2] == '0' && Tekst.Text[i + 3] == '1') sum2++;

                    if (Tekst.Text[i] == '0' && Tekst.Text[i + 1] == '0' && Tekst.Text[i + 2] == '1' && Tekst.Text[i + 3] == '0') sum3++;

                    if (Tekst.Text[i] == '0' && Tekst.Text[i + 1] == '0' && Tekst.Text[i + 2] == '1' && Tekst.Text[i + 3] == '1') sum4++;

                    if (Tekst.Text[i] == '0' && Tekst.Text[i + 1] == '1' && Tekst.Text[i + 2] == '0' && Tekst.Text[i + 3] == '0') sum5++;

                    if (Tekst.Text[i] == '0' && Tekst.Text[i + 1] == '1' && Tekst.Text[i + 2] == '0' && Tekst.Text[i + 3] == '1') sum6++;

                    if (Tekst.Text[i] == '0' && Tekst.Text[i + 1] == '1' && Tekst.Text[i + 2] == '1' && Tekst.Text[i + 3] == '0') sum7++;

                    if (Tekst.Text[i] == '0' && Tekst.Text[i + 1] == '1' && Tekst.Text[i + 2] == '1' && Tekst.Text[i + 3] == '1') sum8++;

                    if (Tekst.Text[i] == '1' && Tekst.Text[i + 1] == '0' && Tekst.Text[i + 2] == '0' && Tekst.Text[i + 3] == '0') sum9++;

                    if (Tekst.Text[i] == '1' && Tekst.Text[i + 1] == '0' && Tekst.Text[i + 2] == '0' && Tekst.Text[i + 3] == '1') sum10++;

                    if (Tekst.Text[i] == '1' && Tekst.Text[i + 1] == '0' && Tekst.Text[i + 2] == '1' && Tekst.Text[i + 3] == '0') sum11++;

                    if (Tekst.Text[i] == '1' && Tekst.Text[i + 1] == '0' && Tekst.Text[i + 2] == '1' && Tekst.Text[i + 3] == '1') sum12++;

                    if (Tekst.Text[i] == '1' && Tekst.Text[i + 1] == '1' && Tekst.Text[i + 2] == '0' && Tekst.Text[i + 3] == '0') sum13++;

                    if (Tekst.Text[i] == '1' && Tekst.Text[i + 1] == '1' && Tekst.Text[i + 2] == '0' && Tekst.Text[i + 3] == '1') sum14++;

                    if (Tekst.Text[i] == '1' && Tekst.Text[i + 1] == '1' && Tekst.Text[i + 2] == '1' && Tekst.Text[i + 3] == '0') sum15++;

                    if (Tekst.Text[i] == '1' && Tekst.Text[i + 1] == '1' && Tekst.Text[i + 2] == '1' && Tekst.Text[i + 3] == '1') sum16++;

                }
                double iloscBlokow = Tekst.Text.Count() / 4;
                
                double suma = (sum1 * sum1 + sum2 * sum2 + sum3 * sum3 + sum4 * sum4 + sum5 * sum5 + sum6 * sum6 + sum7 * sum7 + sum8 * sum8 + sum9 * sum9 + sum10 * sum10 + sum11 * sum11 + sum12 * sum12 + sum13 * sum13 + sum14 * sum14 + sum15 * sum15 + sum16 * sum16);
                double ulamek = 16.0 / iloscBlokow;
                wynik = (ulamek * suma) - iloscBlokow;

                if (wynik > 2.15 && wynik < 46.17)
                {
                WynikiPoker.Text = "zdane";
                }
                else
                {
                WynikiPoker.Text = "niezdane";
            }

            }

        private void WynikiPoker_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        int pfff = 0;
        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            
            System.IO.File.WriteAllText("d:/szyfr/" + "Wynik testu " + pfff+ ".txt", "Wyniki testow dla " +dbf_File + "\r\n" + "Test Bitów " + wynikiBit.Text + " liczba jednynek = " + licz + "\r\n" + 
                "Test Series " + WynikiSeries.Text + " Serie 1 = " + a1 + " Serie 2 = " + a2 + " Serie 3 = " + a3 + " Serie 4 = " + a4 + " Serie 5 = " + a5 + " Serie 6 i wieksze = " + a6 + "\r\n" +
                " Serie 11 = " + b1 + " Serie 21 = " + b2 + " Serie 31 = " + b3 + " Serie 41 = " + b4 + " Serie 51 = " + b5 + " Serie 61 i wieksze = " + b6 + "\r\n" +
                "Test Long " + WynikiLong.Text + " Najdluzsza seria " + max +  "\r\n" +
                "Test Poker " + WynikiPoker.Text + " X = " + wynik + "\n", Encoding.GetEncoding("Windows-1250"));
            Console.WriteLine("zapisano");
            pfff++;
        }
    }
}
