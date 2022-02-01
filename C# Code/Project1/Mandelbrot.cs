using System;
using System.Windows.Forms;
using System.Drawing;

namespace Mandelbrotopdracht
{
    class Mandelbrot : Form
    {
        const double pi = Math.PI;
        
        string kleurenoptie = "Zwart-wit";  // Dit zijn de standaard opties
        double schaal = 0.01;               // De schaal om te converteren van getallen naar pixels
        double xmidden = 0;                 // Het midden van het plaatje wat we gaan tekenen heeft de coordinaten (xmidden, ymidden)
        double ymidden = 0;

        int bitmapsizex = 460;              // Dit wordt de breedte en hoogte van het plaatje dat we gaan tekenen
        int bitmapsizey = 400;

        int maxitr = 100;                   // Het aantal maximaal aantal iteraties dat we uitvoeren voordat we geloven dat een punt in de Mandelbrotverzameling zit

        // Deze moeten buiten de constructormethode worden gedeclareerd zodat ze kunnen worden gelezen en worden geüpdatet buiten de constructormethode (Daar worden ze ook pas geïnitialiseerd)
        TextBox xtext, ytext, schaaltext, maxtext;
        ComboBox kleurenopties;
        Button knop;
        PictureBox fotoBox;

        static void Main()
        {
            Application.Run(new Mandelbrot());
            // De static Main() van ons programma. Hier begint het dus eigenlijk. De constructormethode wordt aangeroepen met 'new', en dat is waar het programma nu naar kijkt.
        }

        public double[] Mandelfunctie(double x, double y, double a, double b)
        {
            // We gebruiken een array van doubles, oftewel we geven een complex getal weer als een rijtje van twee (reële) getallen.
            // De Mandelfunctie f(z) = z^2 + c, waarbij z = x + yi, en c = a + bi. Complexe vermenigvuldiging geeft:
            return new double[] { x*x - y*y + a, 2*x*y + b };
        }

        public double Afstand (double x, double y)
        {
            // Deze methode berekent de afstand van een punt x+yi tot de oorsprong
            return Math.Sqrt(x*x + y*y);
        }
        
        public int Mandelgetal(double cx, double cy)
        {
            // Deze methode bepaalt van een bepaald punt of hij binnen maxitr iteraties divergeert of niet

            int mandelgetal = 0;        // Het aantal iteraties
            int i = 0;                  // De teller/index voor de while loop

            double x = 0, y = 0;        // Dit is onze "iteratie-x en -y"

            while ((i < maxitr) && (Afstand(x,y) <= 2))
            {
                // Hier we declareren en initialiseren een nieuwe array van twee getallen,
                // die onze nieuwe complexe getal geeft
                double[] getal = Mandelfunctie(x, y, cx, cy);
                x = getal[0];
                y = getal[1];

                // Na deze iteratie van de Mandelfunctie verhogen we het mandelgetal en de teller index
                mandelgetal++;
                i++;
            }
            // Als het te lang (langer dan n iteraties) duurt om te divergeren, zeggen we dat het mandelgetal oneindig is, en oneindig geven we hier de waarde -1
            if (mandelgetal == maxitr) return -1;       // Merk op hoe 'else' hier niet nodig is (het programma eindigt toch in ieder geval na één opdracht)

            return mandelgetal;
        }
        
        public Mandelbrot()         // De constructormethode
        {
            this.Text = "Mandelbrot";               // We geven de window een titel
            this.ClientSize = new Size(500, 520);   // en een grootte

            // Hier worden de labels, textvakjes en knoppen gedeclareerd
            Label xlabel, ylabel, schaallabel, maxlabel, uitleglabel;
            xlabel = new Label();
            ylabel = new Label();
            schaallabel = new Label();
            maxlabel = new Label();
            uitleglabel = new Label();
            
            // TextBox xtext, ytext, schaaltext, maxtext;
            xtext = new TextBox();
            ytext = new TextBox();
            schaaltext = new TextBox();
            maxtext = new TextBox();
            
            knop = new Button();
            kleurenopties = new ComboBox();

            // En hier worden ze geïnitialiseerd
            // Eerst de labels
            xlabel.Location = new Point(10, 15);
            ylabel.Location = new Point(10, 40);
            schaallabel.Location = new Point(180, 15);
            maxlabel.Location = new Point(180, 40);
            uitleglabel.Location = new Point(20 ,490);
            xlabel.Text = "Midden X:";
            ylabel.Text = "Midden Y:";
            schaallabel.Text = "Schaal:";
            maxlabel.Text = "Max:";
            uitleglabel.Text = "Linkermuisknop voor inzoomen / rechtermuisknop voor uitzoomen.";
            xlabel.Size = new Size(55, 20);
            ylabel.Size = new Size(55, 20);
            schaallabel.Size = new Size(50, 20);
            maxlabel.Size = new Size(30, 20);
            uitleglabel.Size = new Size(460, 20);
            // En het volgende commando voegt ze daadwerkelijk toe aan de Form
            this.Controls.Add(xlabel);
            this.Controls.Add(ylabel);
            this.Controls.Add(schaallabel);
            this.Controls.Add(maxlabel);
            this.Controls.Add(uitleglabel);


            // Nu de textboxes
            xtext.Location = new Point(70, 12);
            ytext.Location = new Point(70, 37);
            schaaltext.Location = new Point(230, 12);
            maxtext.Location = new Point(230, 37);
            xtext.Text = xmidden.ToString();
            ytext.Text = ymidden.ToString();
            schaaltext.Text = schaal.ToString();
            maxtext.Text = maxitr.ToString();
            xtext.Size = new Size(100, 20);
            ytext.Size = new Size(100, 20);
            schaaltext.Size = new Size(110, 20);
            maxtext.Size = new Size(50, 20);
            // En ze toevoegen
            this.Controls.Add(xtext);
            this.Controls.Add(ytext);
            this.Controls.Add(schaaltext);
            this.Controls.Add(maxtext);

            // Dan nog de teken-knop
            knop.Location = new Point(290, 36);
            knop.Text = "Teken!";
            knop.Size = new Size(50, 20);
            this.Controls.Add(knop);

            // en last but not least de drop-down menu
            kleurenopties.Location = new Point(360, 12);
            kleurenopties.Size = new Size(100, 100);
            kleurenopties.Items.Add("Zwart-wit");
            kleurenopties.Items.Add("Vuur");
            kleurenopties.Items.Add("Zee");
            kleurenopties.Items.Add("Regenboog");
            kleurenopties.SelectedItem = kleurenoptie;
            this.Controls.Add(kleurenopties);

            // We gebruiken een PictureBox om een bitmap in te laten zien
            fotoBox = new PictureBox();
            fotoBox.Location = new Point(20, 80);
            fotoBox.Size = new Size(460, 400);
            this.Controls.Add(fotoBox);

            // Nu willen we dat er iets gebeurt als:

            // we op het scherm klikken;
            fotoBox.MouseClick += this.SchermKlik;    
            // we op de teken-knop klikken;
            knop.Click += this.ButtonKlik;
            // we willen dat er iets wordt getekend! (uiteraard zullen de vorige twee methodes de teken-methode aanroepen met Invalidate())
            this.Paint += this.teken;
        }

        public void ButtonKlik(object o, EventArgs ea)
        {
            // Eerst worden de door de user ingevoerde waardes doorgevoerd (En als de user geen geldige getallen invoert dan catchen we die error en geven een error message aan de user)
            try
            {
                schaal = double.Parse(schaaltext.Text);
                maxitr = int.Parse(maxtext.Text);
                xmidden = double.Parse(xtext.Text);
                ymidden = -double.Parse(ytext.Text); // Dat minteken is omdat de y-as in principe verkeerd om staat. (naar beneden is grotere y)      

                // Nu kijken we welke optie de gebruiker heeft geselecteerd, en roepen we de teken-methode aan
                kleurenoptie = kleurenopties.SelectedItem.ToString();
                this.Invalidate();
            }

            catch (Exception f)
            {
                fotoBox.Paint += ErrorPaint;    // De user heeft (waarschijnlijk) een ongeldig getal ingevoerd, geef dit door aan de user
                fotoBox.Invalidate();
            }
        }

        private void SchermKlik(object o, MouseEventArgs mea)
        {
            xmidden += schaal * (mea.X - 230);         // We veranderen het nieuwe midden van onze tekenbox aan de hand van waar de user heeft geklikt
            ymidden += schaal * (mea.Y - 200);

            xtext.Text = Convert.ToString(xmidden);    // En we updaten ook gelijk de waardes in de textboxen
            ytext.Text = Convert.ToString(-ymidden);   // Dat minteken is omdat de y-as in principe verkeerd om staat. (naar beneden is grotere y)

            if (mea.Button == MouseButtons.Left)       // Linkermuisknop = inzoomen
                schaal *= 0.5;
            if (mea.Button == MouseButtons.Right)      // Rechtermuisknop = uitzoomen
                schaal *= 2.0;

            kleurenoptie = kleurenopties.SelectedItem.ToString(); // We willen ook dat de kleurenoptie wordt geüpdatet zonder op de Teken!-knop te drukken

            schaaltext.Text = Convert.ToString(schaal);

            this.Invalidate();
        }

        private void teken(object o, PaintEventArgs pea)
        {
            // Aangezien we opties hebben voor kleuren, maken we voor iedere optie een net iets andere code
            // We maken gebruik van een switch, aan de hand van de kleurenoptie (die de user heeft gekozen in de drop-down menu) tekenen we de juiste kleuren

            Graphics g = pea.Graphics;  // Afkorting voor de overzichtelijkheid

            Bitmap mandelimg = new Bitmap(bitmapsizex, bitmapsizey);    // De bitmap waarin we de kleuren van de pixels gaan opslaan
            int lokalemandel;           // We gaan per pixel kijken wat het mandelgetal is van die pixel

            switch (kleurenoptie)
            {
                case "Zwart-wit": // De basisoptie, we kijken naar het mandelgetal van een punt. Is het even, dan tekenen we hem wit, anders zwart.

                    for (int x = 0; x < bitmapsizex; x++)
                    {
                        for (int y = 0; y < bitmapsizey; y++)
                        {
                            // Twee for-loops om over alle pixels heen te lopen
                            // Nu kijken we naar het mandelgetal van de huidige x,y
                            lokalemandel = Mandelgetal(xmidden + schaal * (x - 230), ymidden + schaal * (y - 200));

                            // En geven de juiste kleur mee aan deze pixel
                            if (lokalemandel == -1)
                                mandelimg.SetPixel(x, y, Color.Black);
                            else if (lokalemandel % 2 == 1)
                                mandelimg.SetPixel(x, y, Color.Black);
                            else if (lokalemandel % 2 == 0)
                                mandelimg.SetPixel(x, y, Color.White);
                        }
                    }

                    // Nu hebben we alle pixels een kleur gegeven, dus stoppen we bitmap in onze PictureBox (genaamd fotoBox)!
                    fotoBox.Image = mandelimg;
                    break;

                case "Regenboog":   // Een leuke optie met veel kleuren. We gebruiken de sinus van het mandelgetal van een pixel om leuke rgb waardes te vinden.

                    for (int x = 0; x < bitmapsizex; x++)
                    {
                        for (int y = 0; y < bitmapsizey; y++)
                        {
                            lokalemandel = Mandelgetal(xmidden + schaal * (x - 230), ymidden + schaal * (y - 200));

                            if (lokalemandel == -1)                     // Bij oneindig (hadden we gedefinieerd als -1) willen we nog wel zwarte pixels
                                mandelimg.SetPixel(x,y,Color.Black);
                            else
                            {   // RGB values lopen van 0 tot 255, een sinus van -1 tot 1. Met deze transformaties pakken we rgb values tussen de 0 en 254 (goed genoeg dus)
                                // De factor 2 pi / 16 zorgt ervoor dat iedere 16 'lagen' van nieuwe kleuren we weer terug zijn bij af.
                                // Omdat we niet grijze plaatjes willen, is de periode van Red 2 pi / 3 vertraagd t.o.v. Green, die 2 pi / 3 vertraagd is t.o.v. Blue
                                Color lokalekleur = Color.FromArgb((int)(127 * (1 + Math.Sin(2 * pi * lokalemandel / 16))), (int)(127 * (1 + Math.Sin(2 * pi * lokalemandel / 16 + 2 * pi / 3))), (int)(127 * (1 + Math.Sin(2 * pi * lokalemandel / 16 + 4 * pi / 3))));
                                mandelimg.SetPixel(x, y, lokalekleur);
                            }
                        }
                    }

                    fotoBox.Image = mandelimg;

                    break;

                case "Vuur": // Een vroeg experiment met vooral Rood, maar ook een beetje geel.

                    for (int x = 0; x < bitmapsizex; x++)
                    {
                        for (int y = 0; y < bitmapsizey; y++)
                        {
                            lokalemandel = Mandelgetal(xmidden + schaal * (x - 230), ymidden + schaal * (y - 200));

                            if (lokalemandel == -1)   // Zwart voor punten in de Mandelbrotverzameling is nou eenmaal standaard, en ziet er toch altijd mysterieus uit
                                mandelimg.SetPixel(x, y, Color.Black);
                            else
                            {   // Een vroeg experiment met kleuren, maar is uiteindelijk prima genoeg om erin te blijven, namelijk:
                                // we nemen gewoon een factor maal het mandelbrot getal modulo 255. Omdat we ook geel willen, en rood + groen = geel, 
                                // hebben we ook wat groen erbij gedaan (modulo een kleiner getal, we willen vooral roodtinten)
                                Color lokalekleur = Color.FromArgb(16 * lokalemandel % 256, 16 * lokalemandel % 64, 0);
                                mandelimg.SetPixel(x, y, lokalekleur);
                            }
                        }
                    }

                    fotoBox.Image = mandelimg;

                    break;

                case "Zee": // Net als vuur, alleen dan met blauw- en groentinten, en (bijna) nergens zwart/ hele donkere kleuren door arbitraire modulo getallen en voorfactoren
               
                    for (int x = 0; x < bitmapsizex; x++)
                    {
                        for (int y = 0; y < bitmapsizey; y++)
                        {
                            lokalemandel = Mandelgetal(xmidden + schaal * (x - 230), ymidden + schaal * (y - 200));

                            if (lokalemandel == -1)
                                mandelimg.SetPixel(x, y, Color.Black);
                            else
                            {   // Af en toe is de zee iets te groen, maar dat zijn dan algen of zo
                                Color lokalekleur = Color.FromArgb(0, (7 * lokalemandel % 62) + 20, 16 * lokalemandel % 235 + 20);
                                mandelimg.SetPixel(x, y, lokalekleur);
                            }
                        }
                    }

                    fotoBox.Image = mandelimg;

                    break;
            }
            
        }

        private void ErrorPaint(object o, PaintEventArgs pea)     // Weet je nog? Als je een ongeldig getal invoert dan wordt dit aangeroepen!
        {
            Graphics g = pea.Graphics;

            g.FillRectangle(Brushes.White, new Rectangle(95, 175, 270, 50));       // Teken een doosje voor de achtergrond (anders is de tekst niet altijd leesbaar)
            g.DrawString("Voer a.u.b. geldige getallen in.", new Font("Arial", 14), Brushes.Red, new Point(100, 188));     // En vertel aan de user dat die even redelijke getallen moet gaan invoeren

            fotoBox.Paint -= ErrorPaint;
            // Dit moet erbij, namelijk als het eenmaal is gepaint, en de user voert daarna (geldige) getallen in, zouden de twee bovenstaande paint opdrachten nog steeds
            // in het lijstje staan van dingen die het programma moet painten (als een paint-event wordt gegenereerd).
            // Als de user alsnog ongeldige getallen invoert dan worden de bovenstaande twee paint opdrachten weer netjes toegevoegd
            // aan de lijst van dingen die moeten worden gepaint, dan worden ze gepaint, en weer uit het lijstje gehaald.
        }
    }
}
