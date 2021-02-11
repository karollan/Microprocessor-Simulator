using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikroprocesor
{
    //http://spike.scu.edu.au/~barry/interrupts.html#ah36

    static class Interrupt
    {
        public static void InterruptHandler(Mikroprocesor procesor, string interrupt, string ex, int nr)
        {
            switch (interrupt)
            {
                case "INT21":
                    switch (procesor.AH)
                    {
                        case 36: getFreeDiskSpace(procesor); break;
                        case 19: getCurrentDefaultDrive(procesor); break;
                        case 7: readCharacterInput(procesor); break;
                        case 30: getDOSVersion(procesor); break;
                        case 47: getCurrentDirectory(); break;
                        default:
                            Kompilator.Parserr.wystapilBlad = true;
                            Kompilator.Parserr.tablicaBledow.Add($"W linii {nr + 1} \nNie istnieje obsługa przerwania o numerze funkcji wskazanym w rejestrze AH.");
                            procesor.ChangeLineForeColor(Color.Red, ex, nr);
                            break;
                    }
                    break;
                case "INT1A":
                    switch (procesor.AH)
                    {
                        case 2: getRealTimeClockTime(procesor); break;
                        case 4: getRealTimeClockDate(procesor); break;
                        default:
                            Kompilator.Parserr.wystapilBlad = true;
                            Kompilator.Parserr.tablicaBledow.Add($"W linii {nr + 1} \nNie istnieje obsługa przerwania o numerze funkcji wskazanym w rejestrze AH.");
                            procesor.ChangeLineForeColor(Color.Red, ex, nr);
                            break;
                    }
                    break;

                case "INT33":
                    switch (procesor.AX)
                    {
                        case 1: showMousePointer(procesor); break;
                        case 2: hideMousePointer(procesor); break;
                        case 3: getMousePositionAndStatus(procesor); break;
                        default:
                            Kompilator.Parserr.wystapilBlad = true;
                            Kompilator.Parserr.tablicaBledow.Add($"W linii {nr + 1} \nNie istnieje obsługa przerwania o numerze funkcji wskazanym w rejestrze AX.");
                            procesor.ChangeLineForeColor(Color.Red, ex, nr);
                            break;
                    }
                    break;

                case "INT15":
                    switch (procesor.AH)
                    {
                        case 86: Wait(procesor); break;
                        default:
                            Kompilator.Parserr.wystapilBlad = true;
                            Kompilator.Parserr.tablicaBledow.Add($"W linii {nr + 1} \nNie istnieje obsługa przerwania o numerze funkcji wskazanym w rejestrze AH.");
                            procesor.ChangeLineForeColor(Color.Red, ex, nr);
                            break;
                    }
                    break;

                default:
                    Kompilator.Parserr.wystapilBlad = true;
                    Kompilator.Parserr.tablicaBledow.Add($"W linii {nr + 1} \nNie istnieje obsługa podanego przerwania.");
                    procesor.ChangeLineForeColor(Color.Red, ex, nr);
                    break;
            }
        }


        //INT 21H DOS INTERRUPTS

        //Get free disk space  ----> AH = 36h
        //entry DL = drive number (0 = default, 1 = A:, etc)

        //Zwraca
        //AX = FFFFh if invalid drive
        //AX = sectors per cluster, BX = number of free clusters, CX = bytes per sector, DX = total clusters on drive

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetDiskFreeSpace(string lpRootPathName, out int lpSectorsPerCluster, out int lpBytesPerSector, out int lpNumberOfFreeClusters, out int lpTotalNumberOfClusters);

        private static void getFreeDiskSpace(Mikroprocesor procesor)
        {
            char defaultD = Convert.ToChar(Path.GetPathRoot(Environment.SystemDirectory).Substring(0, 1));
            int defaultDrive = Convert.ToInt32(defaultD) % 32;

            bool diskFound = false;

            foreach (var drive in DriveInfo.GetDrives())
            {
                char c = Convert.ToChar(drive.Name.Substring(0, 1));
                int disk = Convert.ToInt32(c) % 32;
                if (disk == procesor.DL || (procesor.DL == 0 && defaultDrive == disk))
                {
                    diskFound = true;
                    GetDiskFreeSpace(drive.Name, out int SectorsPerCluster, out int BytesPerSector, out int NumberOfFreeClusters, out int TotalNumberOfClusters);
                    procesor.AX = (short)SectorsPerCluster;
                    procesor.BX = (short)NumberOfFreeClusters;
                    procesor.CX = (short)BytesPerSector;
                    procesor.DX = (short)TotalNumberOfClusters;

                    MessageBox.Show($"{SectorsPerCluster} sektorów na klaster dyskowy \n{NumberOfFreeClusters} ilość wolnych klastrów dyskowych \n{BytesPerSector} bajtów na sektor \n{TotalNumberOfClusters} całkowita ilość klastrów dyskowych");
                }
            }

            if (!diskFound) procesor.AX = Convert.ToInt16("FFFF", 16);
            procesor.aktualizujRejestry();

        }

        //Get current default drive ----> AH = 19h

        //Return: AL = drive (0 = A:, 1 = B:, etc)

        private static void getCurrentDefaultDrive(Mikroprocesor procesor) 
        {
            char c = Convert.ToChar(Path.GetPathRoot(Environment.SystemDirectory).Substring(0, 1));

            procesor.AL = (sbyte)(Convert.ToInt32(c) % 32);
            procesor.AX = (short)((procesor.AH << 8) | procesor.AL & 0xFF);
            procesor.aktualizujRejestry();
        }

        //Direct character input, without ECHO ---> AH = 07h
        //CZEKA AZ BEDZIE KLAWIATURA KLIKNIETA
        /*
         * MOV AH, 7
         * INT 21
         */
        //Return: AL = character read from standard input

        private async static void readCharacterInput(Mikroprocesor procesor)
        {
            procesor.ReadChar();
        }

        //Get DOS Version ---> AH = 30h
        //Entry: AL = what to return in BH (00h OEM number, 01h version flag)

        //Return: AL = major version number (00h if DOS 1.x)
        //AH = minor version number
        //BL:CX 24-bit user serial number (most versions do not use this) if DOS < 5 or AL = 00
        //BH = MS-DOS OEM number if DOS 5+ and AL = 01h
        //BH = version flag bit 3: DOS is in ROM other: reserver (0)

        private static void getDOSVersion(Mikroprocesor procesor)
        {
            procesor.AH = (sbyte)Environment.OSVersion.Version.Major;
            procesor.AL = (sbyte)Environment.OSVersion.Version.Minor;
            procesor.AX = (short)((procesor.AH << 8) | procesor.AL & 0xFF);
            procesor.CX = (short)Environment.OSVersion.Version.Build;
            if (Environment.Is64BitOperatingSystem)
            {
                procesor.DX = 64;
            }
            else procesor.DX = 32;
            
            procesor.aktualizujRejestry();
        }

        //Get current directory ---> AH = 47
        //Return executable directory to screen

        private static void getCurrentDirectory()
        {
            MessageBox.Show(System.Reflection.Assembly.GetEntryAssembly().Location);
        }


        //INT 1A 

        //Get real-time clock time ----> AH = 02h
        //Return: CH = hour (BCD), CL = minutes (BCD), DH = seconds (BCD), DL = daylight savings flag (00h standard time, 01h daylight time)

        private static void getRealTimeClockTime(Mikroprocesor procesor) 
        {
            var data = DateTime.Now;

            procesor.CH = (sbyte)data.Hour;
            procesor.CL = (sbyte)data.Minute;
            procesor.CX = (short)((procesor.CH << 8) | procesor.CL & 0xFF);

            procesor.DH = (sbyte)data.Second;
            procesor.DL = (sbyte)(TimeZoneInfo.Local.IsDaylightSavingTime(data) ? 1 : 0);
            procesor.DX = (short)((procesor.DH << 8) | procesor.DL & 0xFF);

            procesor.aktualizujRejestry();
        }


        //Get real-time clock date ----> AH = 04h
        //Return: CH = centrury (BCD), CL = year (BCD), DH = month (BCD), DL = day(BCD)

        private static void getRealTimeClockDate(Mikroprocesor procesor)
        {
            var data = DateTime.Now;


            procesor.BL = (sbyte)((data.Year / 100) + ((data.Year % 100 == 0) ? 0 : 1));
            procesor.BX = (short)((procesor.BH << 8) | procesor.BL & 0xFF);

            procesor.CX = (short)data.Year;

            procesor.DH = (sbyte)data.Month;
            procesor.DL = (sbyte)data.Day;
            procesor.DX = (short)((procesor.DH << 8) | procesor.DL & 0xFF);

            procesor.aktualizujRejestry();
        }

        //INT 33h 

        //Show visible mouse pointer ---> AX = 0001
        /*
        * MOV AX, 1
        * INT 33
        */
        private static void showMousePointer(Mikroprocesor procesor)
        {
            Cursor.Show();
            procesor.aktualizujRejestry();
        }

        //Hide visible mouse pointer ---> AX = 0002
        /*
         * MOV AX, 2
         * INT 33
         */

        private static void hideMousePointer(Mikroprocesor procesor)
        {
            Cursor.Hide();
            procesor.aktualizujRejestry();
        }

        //Get mouse position and status of its buttons -----> AX = 0003
        //Return: 
        // if left button down BX = 1
        // if right button down BX = 2
        // if center button down BX = 3   DO IMPLEMENTACJI
        // CX = x
        // DX = y

        /*
         * MOV AX, 3
         * INT 33
         * 
         */

        private static void getMousePositionAndStatus(Mikroprocesor procesor)
        {
            bool leftB = false;
            bool rightB = false;
            bool centerB = false;

            procesor.CX = (short)Cursor.Position.X;
            procesor.DX = (short)Cursor.Position.Y;

            if (Control.MouseButtons == MouseButtons.Right) rightB = true;
            if (Control.MouseButtons == MouseButtons.Left) leftB = true;
            if (Control.MouseButtons == MouseButtons.Middle) centerB = true;

            if (rightB)
            {
                procesor.BX = 2;
            }
            else if (leftB)
            {
                procesor.BX = 1;
            }
            else if (centerB)
            {
                procesor.BX = 3;
            }
            else procesor.BX = 0;

            procesor.aktualizujRejestry();
        }

        //INT 15H

        //Wait ---> AH = 86H

        //CX, DX contain interval in microseconds (CX is high word, DX is low word)
        //If CX and DX are empty no action is taken

        private static void Wait(Mikroprocesor procesor)
        {
            if (procesor.CX != 0 || procesor.DX != 0)
            {
                Thread.Sleep(procesor.CX | procesor.DX);
            }
            procesor.aktualizujRejestry();
        }
    }
}
