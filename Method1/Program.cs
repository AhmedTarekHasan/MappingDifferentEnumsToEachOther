using System;

namespace Method1
{
    public class Program
    {
        static void Main(string[] args)
        {
            var itemSize = ItemSize.Size_30_30;
            Console.WriteLine(itemSize.ToStandardItemSize()); // StandardItemSize.Size30_30

            var standardItemSize = StandardItemSize.Size40_40;
            Console.WriteLine(standardItemSize.ToItemSize()); // ItemSize.Size_40_40

            Console.ReadLine();
        }
    }

    public enum ItemSize
    {
        Size_30_30,
        Size_40_40,
        Size_45_30,
        Size_50_20
    }

    public enum StandardItemSize
    {
        Size30_30,
        Size40_40,
        Size45_30
    }

    public static class ItemSizeConversions
    {
        public static StandardItemSize? ToStandardItemSize(this ItemSize itemSize)
        {
            switch (itemSize)
            {
                case ItemSize.Size_30_30:
                    return StandardItemSize.Size30_30;
                case ItemSize.Size_40_40:
                    return StandardItemSize.Size40_40;
                case ItemSize.Size_45_30:
                    return StandardItemSize.Size45_30;
                default:
                    return null;
            }
        }

        public static ItemSize ToItemSize(this StandardItemSize standardItemSize)
        {
            switch (standardItemSize)
            {
                case StandardItemSize.Size30_30:
                    return ItemSize.Size_30_30;
                case StandardItemSize.Size40_40:
                    return ItemSize.Size_40_40;
                case StandardItemSize.Size45_30:
                    return ItemSize.Size_45_30;
                default:
                    return ItemSize.Size_30_30;
            }
        }
    }
}