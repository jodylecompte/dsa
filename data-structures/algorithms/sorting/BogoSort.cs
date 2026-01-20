namespace data_structures.algorithms.sorting;

/*
 * Bogo sort is a meme
 * A horrifying and hilarious meme
 * Don't use this in production
 */

public static class BogoSort
{
    public static List<int> Sort(List<int> list)
    {
        var rng = new Random();

        while (!IsSorted(list))
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        return list;
    }

    private static bool IsSorted(List<int> list)
    {
        for (int i = 1; i < list.Count; i++)
        {
            if (list[i - 1] > list[i])
                return false;
        }

        return true;
    }
}