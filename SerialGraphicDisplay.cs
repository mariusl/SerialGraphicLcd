using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using System.IO.Ports;

namespace MC.Hardware.SerialGraphicLcd
{
    public sealed class SGLcd
    {

        public enum DisplayType { H12864, H240320 } // will expand later

        public enum Status { On, Off }


        private readonly SerialPort _serialPort;

        private readonly DisplayType _displayType;


        public SGLcd(string portName)
            : this(portName, DisplayType.H12864)
        { }

        public SGLcd(string portName, DisplayType displayType)
        {
            // Defaults for SerialPort are the same as the settings for the LCD, but I'll set them explicitly
            _serialPort = new SerialPort(portName, 115200, Parity.None, 8, StopBits.One);

            _displayType = displayType;
        }

        public void DemoDisplay()
        {
            Write(new byte[] { 0x7C, 0x04 });
        }

        public void ClearDisplay()
        {
            Write(new byte[] { 0x7C, 0x00 });
        }

        // Reverse Mode
        public void Reverse()
        {
            Write(new byte[] { 0x7C, 0x12 });
        }

        //Splash Screen
        public void SplashScreen()
        {
            Write(new byte[] { 0x7C, 0x13 });
        }

        //Set Backlight Duty Cycle
        public void BackLightDutyCycle(byte dutycycle)
        {
            Write(new byte[] {0x7c, 0x02, dutycycle });
        }

        //Change Baud Rate
        /*
        “1” = 4800bps
        “2” = 9600bps
        “3” = 19,200bps
        “4” = 38,400bps
        “5” = 57,600bps
        “6” = 115,200bps
        */
        public void NewBaudRate(string baud)
        {
            Write(new byte[] {0x7c, 0x07,(byte)baud[0] });
        }


        //Set X or Y Coordinates
        public void GotoXY(byte x, byte y)
        {
            Write(new byte[] { 0x7c, 0x18, x });
            Write(new byte[] { 0x7c, 0x19, y });
        }

        //Set/Reset Pixel - x =0 to xmax, y = 0 to ymax, state = 0 or 1
        public void SetPixel(byte x, byte y, byte state)
        {
            Write(new byte[] { 0x7c, 0x10, x, y, state });
        }


        //Draw Line x1 y1 first coords, x2 y2 second coords, state 0 = erase 1 = draw
        public void DrawLine(byte x1, byte y1, byte x2, byte y2, byte state)
        {
            Write(new byte[] { 0x7c, 0x0c, x1, y1, x2, y2, state });
        }


        //Draw Circle
        public void DrawCircle(byte x, byte y, byte r, byte state)
        {
            Write(new byte[] { 0x7c, 0x03, x, y, r, state });
        }

        //Draw Box
        public void DrawBox(byte x1, byte y1, byte x2, byte y2, byte state)
        {
            Write(new byte[] { 0x7c, 0x0f, x1, y1, x2, y2, state });
        }

        //Erase Block
        public void EraseBlock(byte x1, byte y1, byte x2, byte y2)
        {
            Write(new byte[] { 0x7c, 0x05, x1, y1, x2, y2 });
        }


        public void Open()
        {
            if (!_serialPort.IsOpen)
                _serialPort.Open();
        }



        public void Write(byte buffer)
        {
            Write(new[] { buffer });
        }

        public void Write(byte[] buffer)
        {
            Open();

            _serialPort.Write(buffer, 0, buffer.Length);
        }

        public void Write(char character)
        {
            Write((byte)character);
        }

        public void Write(string text)
        {
            byte[] buffer = new byte[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                buffer[i] = (byte)text[i];
            }

            Write(buffer);
        }

        public void WriteXY(byte x, byte y, string text)
        {
            byte[] buffer = new byte[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                buffer[i] = (byte)text[i];
            }
            this.GotoXY(x, y);
            Write(buffer);
        }
    }
}