using System;
using System.Collections.Generic;
using System.Diagnostics;       //for Debug.WriteLine
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections;
using Windows.UI;

/*HOW to PACKAGE APP
*Right click on "AppName" (Universal Windows) in the Solution Explorer Pane
*Publish -> Create App Packages
*I sideloaded the debug version 
*Made custom certificate for Caleb Luebbering
*Generated needed files into selected directory
*Right click on "Add-AppDevPackage" Powershell Command file and run with Powershell
*This should install app to device.
*   Error I got: Package already installed
*   Use these 2 commands... first finds full name of package, second uninstalls selected package
*   *** get-appxpackage -all
*   *** remove-appxpackage -package PackageFullName -AllUsers
*   The PackageFullName for me was: 4c9d7e7b-0a27-4649-a5e8-e86ed0e0e431_1.0.0.0_x86__f105qtj573604
*
*HELPFUL SOURCES:
* https://docs.microsoft.com/en-us/windows/msix/package/packaging-uwp-apps  //overall guide
* https://developercommunity.visualstudio.com/t/another-user-has-already-installed-an-unpackaged-v/198610 //error I had guide
* https://stackoverflow.com/questions/30798749/sharing-windows-universal-app-with-friends   //REMOVING CERTIFICATE REQUIRED!!!
*/

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

//SOME SOURCES USED
//https://docs.microsoft.com/en-us/visualstudio/get-started/csharp/tutorial-uwp?view=vs-2022


namespace WH
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ArrayList wordList;   //MAIN ARRAY WITH ALL WORDS
        private ArrayList updatedList;    //ArrayList to be changed with green grey and yellow letters
        private Boolean countForFirstTimeCallingYellow; //go to yellow letters methods
        private Boolean countForFirstTimeCallingGray;

        private Boolean greenSearch1; //True = filter green letter i
        private Boolean greenSearch2; //*
        private Boolean greenSearch3; //*
        private Boolean greenSearch4; //*
        private Boolean greenSearch5; //*

        private char greenLetter1; //Letter to be filtered if matching search is true
        private char greenLetter2; //*
        private char greenLetter3; //*
        private char greenLetter4; //*
        private char greenLetter5; //*


        public MainPage()
        {

            //Initialize wordList
            Debug.WriteLine("Program starting...");
            wordList = InitWordList();
            updatedList = InitWordList();

            //set first time count = true
            countForFirstTimeCallingYellow = true;
            countForFirstTimeCallingGray = true;

            this.InitializeComponent();
        }

        //***************BUTTONS***************
        private void GreenBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;   //or use keyword 'as'
            char c = '0';
               
            if (tb.Text.Length > 0) //doesnt count blank characters
            {
                c = tb.Text[0];
                if (!(c > 64 && c < 91))
                {
                    tb.Text = "";
                }
            }
            else
            {
                greenSearch1 = false;
            }

            if (c > 64 && c < 91)
            {
                //Debug.WriteLine(c);
                greenSearch1 = true;
                greenLetter1 = c;
            }
            else
            {
                greenSearch1 = false;
            }


        }
        private void GreenBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;   //or use keyword 'as'
            char c = '0';

            if (tb.Text.Length > 0) //doesnt count blank characters
            {
                c = tb.Text[0];
                if (!(c > 64 && c < 91))
                {
                    tb.Text = "";
                }
            }

            if (c > 64 && c < 91)
            {
                //Debug.WriteLine(c);
                greenSearch2 = true;
                greenLetter2 = c;
            }
            else
            {
                greenSearch2 = false;
            }
        }
        private void GreenBox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;   //or use keyword 'as'
            char c = '0';

            if (tb.Text.Length > 0) //doesnt count blank characters
            {
                c = tb.Text[0];
                if (!(c > 64 && c < 91))
                {
                    tb.Text = "";
                }
            }

            if (c > 64 && c < 91)
            {
                //Debug.WriteLine(c);
                greenSearch3 = true;
                greenLetter3 = c;
            }
            else
            {
                greenSearch3 = false;
            }
        }
        private void GreenBox4_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;   //or use keyword 'as'
            char c = '0';

            if (tb.Text.Length > 0) //doesnt count blank characters
            {
                c = tb.Text[0];
                if (!(c > 64 && c < 91))
                {
                    tb.Text = "";
                }
            }

            if (c > 64 && c < 91)
            {
                //Debug.WriteLine(c);
                greenSearch4 = true;
                greenLetter4 = c;
            }
            else
            {
                greenSearch4 = false;
            }
        }
        private void GreenBox5_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;   //or use keyword 'as'
            char c = '0';

            if (tb.Text.Length > 0) //doesnt count blank characters
            {
                c = tb.Text[0];
                if (!(c > 64 && c < 91))
                {
                    tb.Text = "";
                }
            }

            if (c > 64 && c < 91)
            {
                //Debug.WriteLine(c);
                greenSearch5 = true;
                greenLetter5 = c;
            }
            else
            {
                greenSearch5 = false;
            }
        }
        private void FindButton_Click(object sender, RoutedEventArgs e)
        {

            Debug.WriteLine("Beginning Search...");
            beginSearch();
            Debug.WriteLine("Finished Search.");
        }
        public void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            updatedList = resetList(updatedList);
            clearEverything();
            countForFirstTimeCallingYellow = true;
            countForFirstTimeCallingGray = true;
        }
        private void YellowTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Decided to let user enter whatever they want and just give them an error if they are dumb

        }
        private void GreenBox1_GettingFocus(UIElement sender, GettingFocusEventArgs args)
        {
            GreenBox1.Text = "";   
        }
        private void GreenBox2_GettingFocus(UIElement sender, GettingFocusEventArgs args)
        {
            GreenBox2.Text = "";
        }

        private void GreenBox3_GettingFocus(UIElement sender, GettingFocusEventArgs args)
        {
            GreenBox3.Text = "";
        }
        private void GreenBox4_GettingFocus(UIElement sender, GettingFocusEventArgs args)
        {
            GreenBox4.Text = "";
        }
        private void GreenBox5_GettingFocus(UIElement sender, GettingFocusEventArgs args)
        {
            GreenBox5.Text = "";
        }
        private void YellowTextBox_GettingFocus(UIElement sender, GettingFocusEventArgs args)
        {
            if (countForFirstTimeCallingYellow)
                YellowTextBox.Text = "";
            countForFirstTimeCallingYellow = false;
        }
        private void GrayTextBox_GettingFocus(UIElement sender, GettingFocusEventArgs args)
        {
            if (countForFirstTimeCallingGray)
                GrayTextBox.Text = "";
            countForFirstTimeCallingGray = false;
        }
        private void GrayTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //prolly nothing.
        }
        /**
         *  Initializes wordList with the 12,000+ 
         *  possible words. Taken from wordle website...
         *  -right click anywhere, view page source, scroll.
         */
        public ArrayList InitWordList()
        {
            String line;
            ArrayList temp = new ArrayList();
            
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                string filePath = System.IO.Path.GetFullPath("wordle-list.txt");        //FINDS PATH OF wordle-list.txt //does it just have to be in the same dir?
                Debug.WriteLine(filePath);
                StreamReader sr = new StreamReader(filePath);
                //Read the first line of text
                line = sr.ReadLine();
                
                //Continue to read until you reach end of file
                while (line != null)
                {
                    temp.Add(line);
                    line = sr.ReadLine();
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return temp;
        }

        public void beginSearch()
        {
            if (checkIfYellowValid() && checkIfGrayValid())
            {
                //reset previous stuff entered
                updatedList = resetList(updatedList);

                //FILTER GREEN LETTERS
                if (greenSearch1)
                    updateGreen(greenLetter1, 0);
                if (greenSearch2)
                    updateGreen(greenLetter2, 1);
                if (greenSearch3)
                    updateGreen(greenLetter3, 2);
                if (greenSearch4)
                    updateGreen(greenLetter4, 3);
                if (greenSearch5)
                    updateGreen(greenLetter5, 4);

                //FILTER YELLOW
                updateYellow();

                //FILTER GRAY
                updatedGray();

                //Update Answer Possibilities
                updateLetters();

                //Print count
                Debug.WriteLine("\nTotal Possibilities: " + updatedList.Count);
            }
        }

        /*
         * updates green letters in updatedList
         * @param charArg character 
         * @param index index of green letter in word
         */
        public void updateGreen(char c, int index)
        {
            for (int i = 0; i < updatedList.Count; i++)
            {
                String word = (String)updatedList[i];
                if (word[index] != Char.ToLower(c))
                {
                    //Debug.WriteLine("removing: " + word);
                    updatedList.RemoveAt(i);
                    i--;
                }
                    
            }
        }
        /*
         * 
         */
        public void updateYellow()
        {
            String yellowLetters = YellowTextBox.Text;
            yellowLetters = yellowLetters.ToLower();    //*******I AM SO DUMB********
            if (yellowLetters.Length > 0)
            {
                char[] yellowChars = yellowLetters.ToCharArray();
                foreach (char c in yellowChars)
                {
                    for (int i = 0; i < updatedList.Count; i++)
                    {
                        String s = (string)updatedList[i];
                        if ((c != s[0]) && c != s[1] && c != s[2] && c != s[3] && c != s[4])
                        {
                            updatedList.RemoveAt(i);
                            i--;
                        }
                    }
                }
            }
        }
        /*
         * filters out letters in gray
         * text box
         */
        public void updatedGray()
        {
            String grayLetters = GrayTextBox.Text;
            grayLetters = grayLetters.ToLower();    //*******I AM SO DUMB********
            if (grayLetters.Length > 0)
            {
                char[] grayChars = grayLetters.ToCharArray();
                foreach (char c in grayChars)
                {
                    for (int i = 0; i < updatedList.Count; i++)
                    {
                        String s = (string)updatedList[i];
                          //(             any character is equal                          )
                        if (c == s[0] || c == s[1] || c == s[2] || c == s[3] || c == s[4])
                        {
                            //        check if gray letter is already a green letter
                            if (grayIsNotGreen(c))
                            {
                                updatedList.RemoveAt(i);
                                i--;
                            }
                            
                        }
                    }
                }
            }
        }
        /*
         * updates filtered letters and displays to xaml/uwp whatever
         */
        public void updateLetters()
        {
            AnswersTextBlock.Text = "";
            for (int i = 0; i < updatedList.Count; i++)
            {
                AnswersTextBlock.Text += updatedList[i] + ", ";
                if (i == 85)
                    i = updatedList.Count;

            }
            if (AnswersTextBlock.Text.Length > 0)
            AnswersTextBlock.Text = AnswersTextBlock.Text.Remove((AnswersTextBlock.Text.Length-2));
            
        }
        /*
         * checks if yellow and gray letters entered are valid
         * returns true or false
         */
        public Boolean checkIfYellowValid()
        {
            //check yellow letters...
            if (YellowTextBox.Text != "")
            {
                for (int i = 0; i < YellowTextBox.Text.Length; i++)
                {
                    if (!(char.IsLetter(YellowTextBox.Text[i])))
                    {
                        ErrorMessageTextBlock.Visibility = Visibility.Visible;
                        return false;
                    }
                }
                
            }
            ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
            return true;
        }
        public Boolean checkIfGrayValid()
        {
            if (GrayTextBox.Text != "")
            {
                for (int i = 0; i < GrayTextBox.Text.Length; i++)
                {
                    if (!(char.IsLetter(GrayTextBox.Text[i])))
                    {
                        ErrorMessageTextBlock.Visibility = Visibility.Visible;
                        return false;
                    }
                }
                
            }
            ErrorMessageTextBlock.Visibility= Visibility.Collapsed;
            return true;

        }
        public Boolean grayIsNotGreen(char c)
        {
            c = char.ToUpper(c);
            if (GreenBox1.Text != "")
            {
                if (c == GreenBox1.Text[0])
                {
                    return false;
                }
            }
            if (GreenBox2.Text != "")
            {
                if (c == GreenBox2.Text[0])
                {
                    return false;
                }
            }
            if (GreenBox3.Text != "")
            {
                if (c == GreenBox3.Text[0])
                {
                    return false;
                }
            }
            if (GreenBox4.Text != "")
            {
                if (c == GreenBox4.Text[0])
                {
                    return false;
                }
            }
            if (GreenBox5.Text != "")
            {
                if (c == GreenBox5.Text[0])
                {
                    return false;
                }
            }
            
            return true;
        }

        /*
         * prints the current filtered list to debug
         * @param temp the arrayList to be printed
         */
        public void printList(ArrayList temp)
        {
            
            foreach (String word in temp)
                Debug.Write(word + ", ");
            Debug.Write("\n");
        }
        
        /*
         * calls from ResetButton_Click - cleats all user entered text
         */
        public void clearEverything()
        {
            GreenBox1.Text = "";
            GreenBox2.Text = "";
            GreenBox3.Text = "";
            GreenBox4.Text = "";
            GreenBox5.Text = "";

            AnswersTextBlock.Text = "Possible Answers:";
            YellowTextBox.Text = "enter yellow letters:";
            GrayTextBox.Text = "enter gray letters:";
            ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
            
        }
        /*
         * resets updatedList back to wordList
         */
        public ArrayList resetList(ArrayList temp)
        {
            temp.Clear();
            foreach (String word in wordList)
            {
                temp.Add(word);
            }
            return temp;

        }

    }
}