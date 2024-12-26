using System;
using System.Collections.Generic;
using System.Linq;

public class WordFinder
{
    private readonly List<string> _matrix;

    public WordFinder(IEnumerable<string> matrix)
    {
        _matrix = matrix.ToList();
    }

    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
        return wordCounter(wordstream).OrderByDescending(kvp => kvp.Value)
                        .Take(10)
                        .Select(kvp => kvp.Key);
    }

    private Dictionary<string, int> wordCounter(IEnumerable<string> wordstream)
    {
        int rows = _matrix.Count;
        int cols = _matrix[0].Length;
        int count = 0;
        IEnumerable<string> words = wordstream.Distinct();
        Dictionary<string, int> wordCount = new Dictionary<string, int>();

        // Horizontal Search, row by row
        for (int row = 0; row < rows; row++)
        {
            foreach (var word in words)
            {
                count = CountOccurrences(_matrix[row], word);

                if (count > 0)
                {
                    //if the word is not in the dictionary, it is added to it, with the ocurrence count (which could be more than one), if it already exists, only the number of ocurrences are added.
                    if (!wordCount.ContainsKey(word))
                        wordCount[word] = count;
                    else
                        wordCount[word] = wordCount[word] + count;
                }

                count = 0;
            }
        }

        // Vertical Search, column by column
        for (int col = 0; col < cols; col++)
        {
            var column = new string(Enumerable.Range(0, rows).Select(r => _matrix[r][col]).ToArray());

            foreach (var word in words)
            {
                count = CountOccurrences(column, word);

                if (count > 0)
                {
                    if (!wordCount.ContainsKey(word))
                        wordCount[word] = count;
                    else
                        wordCount[word] = wordCount[word] + count;
                }
                
                count = 0;
            }
        }

        return wordCount;
    }

    public static int CountOccurrences(string text, string substring)
    {
        int count = 0;
        int index = 0;

        while ((index = text.IndexOf(substring, index)) != -1)
        {
            count++;
            index += substring.Length;
        }
        // if there is a match, the search continues with the index right after it, to prevent overlapping matches.
        return count;
    }

    public static void Main(string[] args)
    {
        var matrix = new List<string>
        {
            "stubdefabcdev",
            "ghijalghiaklb",
            "mntprrmntrqrq",
            "sghykdayyzgxt",
            "lzabcaayzbmdg",
            "efghiyeaghijh",
            "abcdefarjsnoa",
            "nhijklgvejklr",
            "varvrwvtttaer",
            "iycgxsghyzxao",
            "lcebcdyzabcrw",
            "efghijefghina",
            "kyjflowersjns"
        };

        var wordstream = new List<string>
        {
            "car", "mink", "harrow", "flowers", "stub", "car","anvil","jet","day","circe","dark","learn"
        };

        var wordFinder = new WordFinder(matrix);
        var foundWords = wordFinder.Find(wordstream);

        int wordRank = 1;
        Console.WriteLine("Top 10 found words:");
        foreach (var word in foundWords)
        {
            Console.WriteLine($"{wordRank}) {word}");
            wordRank++;
        }
    }
}

//Considerations:
// There are 2 main ways to resolve this: one is searching each word in the matrix, which would work best if the matrix is small,
// and the second way, which consists in searching the entire word list, one row at a time, and then each column, which is more versatile,
// and would outperform the first approach, so this is the one I chose.