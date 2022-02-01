using System;
using System.Linq;

namespace Method3
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
        [RelatedStandardItemSize(StandardItemSize.Size30_30)]
        Size_30_30,

        [RelatedStandardItemSize(StandardItemSize.Size40_40)]
        Size_40_40,

        [RelatedStandardItemSize(StandardItemSize.Size45_30)]
        Size_45_30,
        Size_50_20
    }

    public enum StandardItemSize
    {
        [RelatedItemSize(ItemSize.Size_30_30)] Size30_30,
        [RelatedItemSize(ItemSize.Size_40_40)] Size40_40,
        [RelatedItemSize(ItemSize.Size_45_30)] Size45_30
    }

    public class RelatedItemSizeAttribute : Attribute
    {
        public ItemSize RelatedItemSize { get; }

        public RelatedItemSizeAttribute(ItemSize relatedItemSize)
        {
            RelatedItemSize = relatedItemSize;
        }
    }

    public class RelatedStandardItemSizeAttribute : Attribute
    {
        public StandardItemSize RelatedStandardItemSize { get; }

        public RelatedStandardItemSizeAttribute(StandardItemSize relatedStandardItemSize)
        {
            RelatedStandardItemSize = relatedStandardItemSize;
        }
    }

    public static class ItemSizeConversions
    {
        public static ItemSize? ToItemSize(this StandardItemSize standardItemSize)
        {
            var memberInfo =
                typeof(StandardItemSize)
                    .GetMember(standardItemSize.ToString())
                    .FirstOrDefault();

            if (memberInfo != null)
            {
                RelatedItemSizeAttribute attribute =
                    memberInfo
                        .GetCustomAttributes(typeof(RelatedItemSizeAttribute), false)
                        .FirstOrDefault() as RelatedItemSizeAttribute;

                return attribute.RelatedItemSize;
            }

            return null;
        }

        public static StandardItemSize? ToStandardItemSize(this ItemSize itemSize)
        {
            var memberInfo =
                typeof(ItemSize)
                    .GetMember(itemSize.ToString())
                    .FirstOrDefault();

            if (memberInfo != null)
            {
                RelatedStandardItemSizeAttribute attribute =
                    memberInfo
                        .GetCustomAttributes(typeof(RelatedStandardItemSizeAttribute), false)
                        .FirstOrDefault() as RelatedStandardItemSizeAttribute;

                return attribute.RelatedStandardItemSize;
            }

            return null;
        }
    }
}