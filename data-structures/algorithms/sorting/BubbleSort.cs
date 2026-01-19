namespace data_structures.algorithms.sorting;

public static class BubbleSort
{
    public static List<int> Sort(List<int> list)
    {
        for (var i = 0; i < list.Count(); i++)
        {
            for (var j = 1; j < list.Count() - i; j++)
            {
                if (list[j - 1] > list[j])
                {
                    (list[j - 1], list[j]) = (list[j], list[j - 1]);
                }
            }
        }

        return list;
    }
}