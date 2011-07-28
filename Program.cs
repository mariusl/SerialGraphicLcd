using System.Collections;
using MC.Hardware.WindowLibrary;
using Microsoft.SPOT;
using MC.Hardware.SerialGraphicLcd;
using System.Threading;

namespace DisplayTest
{
    public class Program
    {
        public static SGLcd Screen = new SGLcd("COM1", 0);
        public static Window winTestResults = new Window(Screen);
        
         public static void Main()
        {
            //SGLcd Screen = new SGLcd("COM1", 0);
            //var display = new Window(Screen);
            //display.Add(new Circle("circle1", 10, 10, 10,1,"text"));
            //display.Add(new Rectangle("rect1", 30, 40, 50, 60,"text"));
            //display.Add(new TextArea("text", 22, 33,0,0, "hello World"));
            //Screen.ClearDisplay();
            //Screen.DemoDisplay();
             //display.DrawAll();
            Thread.Sleep(1000);
            //Screen.EraseBlock(0, 0, 128, 64);
            Screen.ClearDisplay();
            buildTestResultsPage(winTestResults);
            var p = winTestResults.Search("tbSpeed");
             p.Text = "1010";  // this does not work yet. I got stuck here last. I am trying to write to the value at the screen location
                                // in the class but the base value does not change
             
            winTestResults.DrawAll();
            //Screen.GotoXY(0, 0);

            do
            {


            } while (true);

/*
            var pageOne = new Window(Screen);
            pageOne.Add(new TextArea("heading",10,10,0,0,"Page One"));
            //Screen.ClearDisplay();
            pageOne.DrawAll();
            Thread.Sleep(1000);

            var pageTwo = new Window(Screen);
            pageTwo.Add(new TextArea("heading", 10, 56, 0, 0, "Page TWO"));
            //Screen.ClearDisplay();
            pageTwo.DrawAll();
            Thread.Sleep(1000);
            
             // Calling the display driver class directly to do some tricks
            Screen.WriteXY(1, 8, pageOne.Search("heading").Text.ToString());
            // retreiving some of the object information and using it to write some data from another object
            var p = display.Search("rect1");
            Screen.WriteXY(p.X , p.Y , pageOne.Search("heading").Text.ToString());
             */

            //advanced section I: move every widget by (20,20)
            /*
            display.ForEach(w =>
            {
                w.X += 5;
                w.Y += 5;
            });
            */
            //advanced section II: find all circles and double their radius
            /*
            display.ForEach(w =>
            {
                var c = w as Circle;
                if (c != null && c.Name == "circle1")
                {
                    c.Radius += 5;
                }
            });
            */

        }

        public static void buildTestResultsPage(Window page)
        {
            page.Add(new TextArea("pageHeading", 1, 63, 0, 0, "Rewinder Test results"));
            page.Add(new Line("headingUnderLine", 1, 55, 126, 55, "1"));
            page.Add(new TextArea("lblSpeed", 1, 45, 20, 10, "Speed"));
            page.Add(new TextBox("tbSpeed", 40, 46,20,10, "0000"));
            page.DrawAll();
        }


        
    }
}



