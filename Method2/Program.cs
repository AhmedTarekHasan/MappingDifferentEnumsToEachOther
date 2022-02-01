using System;
using System.Linq;
using System.Reflection;

namespace Method2
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
            var membersInfo =
                typeof(StandardItemSize)
                    .GetMembers();

            if (membersInfo.Any())
            {
                MemberInfo found = null;

                foreach (var mi in membersInfo)
                {
                    var relatedItemSize =
                        ((RelatedItemSizeAttribute)
                            mi
                                .GetCustomAttributes(typeof(RelatedItemSizeAttribute), false)
                                .FirstOrDefault())?
                        .RelatedItemSize;

                    if (relatedItemSize == itemSize)
                    {
                        found = mi;
                        break;
                    }
                }

                if (found != null)
                {
                    return (StandardItemSize)Enum.Parse(typeof(StandardItemSize), found.Name);
                }
            }

            return null;
        }
    }
}