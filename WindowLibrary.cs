using System;
using Microsoft.SPOT;
using MC.Hardware.SerialGraphicLcd;
using System.Collections;

namespace MC.Hardware.WindowLibrary
{
    public class Window
    {
        private readonly ArrayList widgets = new ArrayList();
        private SGLcd lcd;

        public Window(SGLcd Lcd)
        {
            lcd = Lcd;
        }


        public void Add(Widget widget)
        {
            widgets.Add(widget);
        }

        public void Delete(string name)
        {
            foreach (Widget widget in widgets)
            {
                if (widget != null && widget.Name == name)
                {
                    widgets.Remove(widget);
                    lcd.ClearDisplay();
                    this.DrawAll();
                }
            }
        }

        public void DrawAll()
        {
            this.ForEach(w => w.Draw(lcd));
        }

        public Widget Search(string id)
        {
            Widget wid = null;
            foreach (Widget widget in widgets)
            {
                if (widget != null && widget.Name == id)
                {
                    wid = widget;
                }
            }
            return (wid);
        }

        public delegate void WidgetAction(Widget widget);


        public void ForEach(WidgetAction action)
        {
            foreach (Widget widget in widgets)
            {
                action(widget);
            }
        }
    }

    public abstract class Widget
    {
        public string Name { get; set; }
        public byte X { get; set; }
        public byte Y { get; set; }
        public byte D { get; set; }
        public string Text { get; set; }

        protected Widget(string name, byte x, byte y, byte d, byte state, string text)
        {
            Name = name;
            X = x;
            Y = y;
            D = d;
            Text = text;
        }

        public abstract void Draw(SGLcd lcd);
    }

    

    public sealed class Circle : Widget
    {
        public byte Radius { get; set; }

        public Circle(string name, byte x, byte y, byte radius, byte state, string text)
            : base(name, x, y, radius, state, text)
        {
            Radius = radius;
        }

        public override void Draw(SGLcd lcd)
        {
            lcd.DrawCircle(X, Y, Radius, 1);
        }
    }


    public sealed class Line : Widget
    {
        public byte X2 { get; set; }
        public byte Y2 { get; set; }

        public Line(string name, byte x, byte y, byte x2, byte y2, string state)
            : base(name, x, y, x2, y2, state)
        {
            X2 = x2;
            Y2 = y2;
        }

        public override void Draw(SGLcd lcd)
        {
            lcd.DrawLine(X, Y, X2, Y2, 1);
        }
    }

    public sealed class Rectangle : Widget
    {
        public byte Width { get; set; }
        public byte Height { get; set; }

        public Rectangle(string name, byte x, byte y, byte width, byte height, string state)
            : base(name, x, y, width, height, state)
        {
            Width = width;
            Height = height;
        }

        public override void Draw(SGLcd lcd)
        {
            lcd.DrawBox(X, Y, Width, Height, 1);
        }
    }


    public sealed class TextArea : Widget
    {
        public string Text { get; set; }
        public TextArea(string name, byte x, byte y, byte d1, byte d2, string text)
            : base(name, x, y, d1, d2, text)
        {
            Text = text;
        }

        public override void Draw(SGLcd lcd)
        {
            lcd.GotoXY(X, Y);
            lcd.Write(Text);
        }
    }


    public sealed class TextBox : Widget
    {
        public string Text { get; set; }
        public byte D1 { get; set; }
        public byte D2 { get; set; }
        public byte Xos { get; set; }
        public byte Yos { get; set; }
      
        public TextBox(string name, byte x, byte y, byte d1, byte d2, string text)
            : base(name, x, y, d1, d2, text)
        {
           
            Text = text;
            
            D1 = d1;
            D2 = Y;
            Xos = X;
            Yos = Y;
          
            D1 += (byte)X;
            D1 += (byte)4;
            D2 += (byte)2;
           // D2 -= d2;
            Xos -= (byte)2;
            Yos -= (byte)8;
        }

        public override void Draw(SGLcd lcd)
        {
            lcd.GotoXY(X, Y);
            lcd.Write(Text);
            lcd.DrawBox(Xos, Yos, D1, D2, 1);
        }
    }

}