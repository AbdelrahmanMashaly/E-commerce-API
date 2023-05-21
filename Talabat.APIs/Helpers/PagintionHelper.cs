using Talabat.APIs.Dtos;

namespace Talabat.APIs.Helpers
{
    public class PagintionHelper<T>
    {
        public int Index { get; set; }

        public int PageSize { get; set; }

        public int Count { get; set; }

        public IReadOnlyList<T> Data { get; set; }

        public PagintionHelper(int index, int pageSize, int count ,IReadOnlyList<T> data)
        {
            Index = index;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
    }
}
