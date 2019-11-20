using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace BabbleSample
{
    /// Babble framework
    public partial class MainWindow : Window
    {
        private string input;                              // input file
        private string[] words;                            // input file broken into array of words
        private int wordCount = 200;                       // number of words to babble
        private Random random_index = new Random();        // random type to determine random number
        private int n = 0;                                 // int variable to keep track
        private string current_word;                       // string variable to keep track of the current_word 
        char[] delimiterChars = { '-' };                   // array only containing the hypen character, used to split the key word
        string[] split_words;                              // array that contains the split words from the key word
        int numWords = 0;                                  // int that represents the number of words in text file
        int numKeys = 0;                                   // int that represents the number of unique words in text file

        public MainWindow()
        {
            InitializeComponent();
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.FileName = "Sample";                        // default file name
            ofd.DefaultExt = ".txt";                        // default file extension
            ofd.Filter = "Text documents (.txt)|*.txt";     // filter files by extension

            // Show open file dialog box
            if ((bool)ofd.ShowDialog())
            {
                textBlock1.Text = "Loading file " + ofd.FileName + "\n";
                input = System.IO.File.ReadAllText(ofd.FileName);        // read file
                words = Regex.Split(input, @"\s+");                      // split into array of words
            }
        }

        private void analyzeInput(int order)
        {
            if (order > 0)
            {
                MessageBox.Show("Analyzing at order: " + order);
            }
        }

        // function that determines what order is called and calls the corresponding order function
        private void babbleButton_Click(object sender, RoutedEventArgs e)
        {
            // clear the current words in the display
            textBlock1.Text = String.Empty;

            // clear the number of words and number of unique words
            countNumbers.Text = " ";
            numKeys = 0;
            numWords = 0;

            // order 0
            if (n == 0)
            {
                for (int i = 0; i < Math.Min(wordCount, words.Length); i++)
                {
                    textBlock1.Text += " " + words[i];
                }
            }
            // order 1
            if (n == 1)
            {
                // create a hashTable to put all the words as a key, and the following word as a value.
                Dictionary<string, ArrayList> hashTable = new Dictionary<string, ArrayList>();

                // enter all the words and the following word as key, value pairs
                for (int i = 0; i < words.Count() - 1; i++)
                {
                    numWords++;
                    string firstword = words[i];
                    if (!hashTable.ContainsKey(firstword))
                    {
                        hashTable.Add(firstword, new ArrayList());
                        numKeys++;
                    }

                    hashTable[firstword].Add(words[i + 1]);
                }

                // print out number of keys and words
                countNumbers.Text += Convert.ToString(numKeys) + " number of keys and " + Convert.ToString(numWords) + " number of words";

                // choose a starting point to write out to the window screen
                current_word = words[0];
                textBlock1.Text += " " + current_word;

                // for loop that prints out 200 words of "random" text from the file, based on the keys and their values
                for (int i = 0; i < Math.Min(wordCount, words.Length); i++)
                {
                    string random_word = Babble_Function(hashTable);
                    textBlock1.Text += " " + random_word;
                    current_word = random_word;
                }
            }

            // order 2
            if (n == 2)
            {
                // create a hashTable to put all the words as a key, and the following word as a value.
                Dictionary<string, ArrayList> hashTable = new Dictionary<string, ArrayList>();

                // enter all the 2 word pairs (that appear in a row) as keys and the following word as its value
                for (int i = 0; i < words.Count() - 2; i++)
                {
                    string firstword = words[i] + "-" + words[i + 1];
                    numWords++;
                    if (!hashTable.ContainsKey(firstword))
                    {
                        hashTable.Add(firstword, new ArrayList());
                        numKeys++;
                    }

                    hashTable[firstword].Add(words[i + 2]);
                }

                // print out number of keys and words
                countNumbers.Text += Convert.ToString(numKeys) + " number of keys and " + Convert.ToString(numWords) + " number of words";

                // choose a starting point to write out to the window screen
                current_word = words[0] + " " + words[1];
                textBlock1.Text += " " + current_word;

                current_word = words[0] + "-" + words[1];

                // for loop to write out 200 "random" words from the text file based on keys and their values
                for (int i = 0; i < Math.Min(wordCount - 1, words.Length); i++)
                {
                    string random_word = Babble_Function(hashTable);
                    textBlock1.Text += " " + random_word;
                    split_words = current_word.Split(delimiterChars);
                    current_word = split_words[1] + "-" + random_word;
                }
            }

            // order 3
            if (n == 3)
            {
                // create a hashTable to put all the words as a key, and the following word as a value.
                Dictionary<string, ArrayList> hashTable = new Dictionary<string, ArrayList>();

                // enter all the 3 word pairs (that appear in a row) as keys and the following word as its value
                for (int i = 0; i < words.Count() - 3; i++)
                {
                    string firstword = words[i] + "-" + words[i + 1] + "-" + words[i + 2];
                    numWords++;
                    if (!hashTable.ContainsKey(firstword))
                    {
                        hashTable.Add(firstword, new ArrayList());
                        numKeys++;
                    }

                    hashTable[firstword].Add(words[i + 3]);
                }

                // print out number of keys and words
                countNumbers.Text += Convert.ToString(numKeys) + " number of keys and " + Convert.ToString(numWords) + " number of words";

                // choose a starting point to write out to the window screen
                current_word = words[0] + " " + words[1] + " " + words[2];
                textBlock1.Text += " " + current_word;

                current_word = words[0] + "-" + words[1] + "-" + words[2];

                // for loop that prints out 200 "random" words from the text file, based on their key and value pairs
                for (int i = 0; i < Math.Min(wordCount - 2, words.Length); i++)
                {
                    string random_word = Babble_Function(hashTable);
                    textBlock1.Text += " " + random_word;
                    split_words = current_word.Split(delimiterChars);
                    current_word = split_words[1] + "-" + split_words[2] + "-" + random_word;
                }
            }

            // order 4
            if (n == 4)
            {
                // create a hashTable to put all the words as a key, and the following word as a value.
                Dictionary<string, ArrayList> hashTable = new Dictionary<string, ArrayList>();

                // enter all the 4 word pairs (that appear in a row) as keys and the following word as its value
                for (int i = 0; i < words.Count() - 4; i++)
                {
                    string firstword = words[i] + "-" + words[i + 1] + "-" + words[i + 2] + "-" + words[i + 3];
                    numWords++;
                    if (!hashTable.ContainsKey(firstword))
                    {
                        hashTable.Add(firstword, new ArrayList());
                        numKeys++;
                    }

                    hashTable[firstword].Add(words[i + 4]);
                }

                // print out number of keys and words
                countNumbers.Text += Convert.ToString(numKeys) + " number of keys and " + Convert.ToString(numWords) + " number of words";

                // choose a starting point to write out to the window screen
                current_word = words[0] + " " + words[1] + " " + words[2] + " " + words[3];
                textBlock1.Text += " " + current_word;

                current_word = words[0] + "-" + words[1] + "-" + words[2] + "-" + words[3];

                // for loop that prints out 200 "random" words from the text value, based on keys and their values
                for (int i = 0; i < Math.Min(wordCount - 3, words.Length); i++)
                {
                    string random_word = Babble_Function(hashTable);
                    textBlock1.Text += " " + random_word;
                    split_words = current_word.Split(delimiterChars);
                    current_word = split_words[1] + "-" + split_words[2] + "-" + split_words[3] + "-" + random_word;
                }
            }

            // order 5
            if (n == 5)
            {
                // create a hashTable to put all the words as a key, and the following word as a value.
                Dictionary<string, ArrayList> hashTable = new Dictionary<string, ArrayList>();

                // enter all the 5 word pairs (that appear in a row) as keys and the following word as its value
                for (int i = 0; i < words.Count() - 5; i++)
                {
                    string firstword = words[i] + "-" + words[i + 1] + "-" + words[i + 2] + "-" + words[i + 3] + "-" + words[i + 4];
                    numWords++;
                    if (!hashTable.ContainsKey(firstword))
                    {
                        hashTable.Add(firstword, new ArrayList());
                        numKeys++;
                    }
                    hashTable[firstword].Add(words[i + 5]);
                }

                // print out number of keys and words
                countNumbers.Text += Convert.ToString(numKeys) + " number of keys and " + Convert.ToString(numWords) + " number of words";

                // choose a starting point to write out to the window screen
                current_word = words[0] + " " + words[1] + " " + words[2] + " " + words[3] + " " + words[4];
                textBlock1.Text += " " + current_word;

                current_word = words[0] + "-" + words[1] + "-" + words[2] + "-" + words[3] + "-" + words[4];

                // for loop that prints out 200 "random" words from the text file, based on keys and their values
                for (int i = 0; i < Math.Min(wordCount - 4, words.Length); i++)
                {
                    string random_word = Babble_Function(hashTable);
                    textBlock1.Text += " " + random_word;
                    split_words = current_word.Split(delimiterChars);
                    current_word = split_words[1] + "-" + split_words[2] + "-" + split_words[3] + "-" + split_words[4] + "-" + random_word;
                }
            }
        }


        // Babble Function chooses a random word from the ArrayList corresponding to a particular key
        private string Babble_Function(Dictionary<string, ArrayList> hashTable)
        {
            // for each order, if key is not in the hash table, start over at the first n words
            if (n == 1)
            {
                if (!hashTable.ContainsKey(current_word))
                { current_word = words[0]; }
            }

            if (n == 2)
            {
                if (!hashTable.ContainsKey(current_word))
                { current_word = words[0] + " " + words[1]; }
            }

            if (n == 3)
            {
                if (!hashTable.ContainsKey(current_word))
                { current_word = words[0] + " " + words[1] + " " + words[2]; }
            }

            if (n == 4)
            {
                if (!hashTable.ContainsKey(current_word))
                { current_word = words[0] + " " + words[1] + " " + words[2] + " " + words[3]; }
            }

            if (n == 5)
            {
                if (!hashTable.ContainsKey(current_word))
                { current_word = words[0] + " " + words[1] + " " + words[2] + " " + words[3] + " " + words[4]; }
            }

            // generate a random number according to how many elements are in the ArrayList
            int number_choice = random_index.Next(hashTable[current_word].Count);

            // create a new ArrayList for the specified key from the hashTable.
            ArrayList list = hashTable[current_word];

            // create a string variable for the random number choice from the ArrayList
            string new_word = Convert.ToString(list[number_choice]);

            // return the randomly choosen word from the ArrayList
            return new_word;

        }

        private void orderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            n = orderComboBox.SelectedIndex;
            analyzeInput(orderComboBox.SelectedIndex);
        }
    }
}
