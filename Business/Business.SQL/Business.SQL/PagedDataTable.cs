﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Business.SQL
{
    [CollectionDataContract]
    public class PagedDataTable<T> : List<T>
    {
        public PagedDataTable()
        {
        }

        /// <summary>
        /// PagedList take four input parameter 
        /// </summary>
        /// <param name="TotalItem">Total Number of record</param>
        /// <param name="pageIndex">Current page index</param>
        /// <param name="pageSize">total number of page size</param>
        public PagedDataTable(int pageIndex, int pageSize, int TotalItem)
        {
            PageSize = pageSize;
            TotalItemCount = TotalItem;
            TotalPageCount = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
            CurrentPageIndex = pageIndex;
            StartRecordIndex = (CurrentPageIndex - 1) * PageSize + 1;
            EndRecordIndex = TotalItemCount > pageIndex * pageSize ? pageIndex * pageSize : TotalItemCount;
            SearchText = string.Empty;
        }
        /// <summary>
        /// PagedList take four input parameter 
        /// </summary>
        /// <param name="TotalItem">Total Number of record</param>
        /// <param name="pageIndex">Current page index</param>
        /// <param name="pageSize">total number of page size</param>
        public PagedDataTable(int pageIndex, int pageSize, int TotalItem, string Search)
        {
            PageSize = pageSize;
            TotalItemCount = TotalItem;
            TotalPageCount = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
            CurrentPageIndex = pageIndex;
            StartRecordIndex = (CurrentPageIndex - 1) * PageSize + 1;
            EndRecordIndex = TotalItemCount > pageIndex * pageSize ? pageIndex * pageSize : TotalItemCount;
            SearchText = Search;
        }
        /// <summary>
        /// PagedList take four input parameter 
        /// </summary>
        /// <param name="items">Total Number of record</param>
        /// <param name="pageIndex">Current page index</param>
        /// <param name="pageSize">total number of page size</param>
        public PagedDataTable(IList<T> items, int pageIndex, int pageSize)
        {
            PageSize = pageSize;
            TotalItemCount = items.Count;
            TotalPageCount = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
            CurrentPageIndex = pageIndex;
            StartRecordIndex = (CurrentPageIndex - 1) * PageSize + 1;
            EndRecordIndex = TotalItemCount > pageIndex * pageSize ? pageIndex * pageSize : TotalItemCount;
            SearchText = string.Empty;
            AddRange(items);
        }
        /// <summary>
        /// PagedList take four input parameter 
        /// </summary>
        /// <param name="items">Total Number of record</param>
        /// <param name="pageIndex">Current page index</param>
        /// <param name="pageSize">total number of page size</param>
        /// <param name="search">search in record</param>
        public PagedDataTable(IList<T> items, int pageIndex, int pageSize, string search)
        {
            PageSize = pageSize;
            TotalItemCount = items.Count;
            TotalPageCount = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
            CurrentPageIndex = pageIndex;
            StartRecordIndex = (CurrentPageIndex - 1) * PageSize + 1;
            EndRecordIndex = TotalItemCount > pageIndex * pageSize ? pageIndex * pageSize : TotalItemCount;
            SearchText = search;
            AddRange(items);
        }
        /// <summary>
        /// PagedList take 5 input parameters 
        /// </summary>
        /// <param name="items">Total Number of record</param>
        /// <param name="pageIndex">Current page index</param>
        /// <param name="pageSize">total number of page size</param>
        /// <param name="totalItemCount">Total number of record count</param>
        public PagedDataTable(IList<T> items, int pageIndex, int pageSize, int totalItemCount)
        {
            PageSize = pageSize;
            TotalItemCount = totalItemCount;
            TotalPageCount = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
            CurrentPageIndex = pageIndex;
            StartRecordIndex = (CurrentPageIndex - 1) * PageSize + 1;
            EndRecordIndex = TotalItemCount > pageIndex * pageSize ? pageIndex * pageSize : TotalItemCount;
            SearchText = string.Empty;
            AddRange(items);
        }
        /// <summary>
        /// PagedList take 5 input parameters 
        /// </summary>
        /// <param name="items">Total Number of record</param>
        /// <param name="pageIndex">Current page index</param>
        /// <param name="pageSize">total number of page size</param>
        /// <param name="totalItemCount">Total number of record count</param>
        /// <param name="search">search in record</param>
        public PagedDataTable(IList<T> items, int pageIndex, int pageSize, int totalItemCount, string search)
        {
            PageSize = pageSize;
            TotalItemCount = totalItemCount;
            TotalPageCount = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
            CurrentPageIndex = pageIndex;
            StartRecordIndex = (CurrentPageIndex - 1) * PageSize + 1;
            EndRecordIndex = TotalItemCount > pageIndex * pageSize ? pageIndex * pageSize : TotalItemCount;
            SearchText = search;
            AddRange(items);
        }

        /// <summary>
        /// PagedList take 6 input parameters 
        /// </summary>
        /// <param name="pageIndex">Current page index</param>
        /// <param name="pageSize">total number of page size</param>
        /// <param name="totalItemCount">Total number of record count</param>
        /// <param name="search">search in record</param>
        /// <param name="orderBy">orderBy for record</param>
        /// <param name="sortBy">sortBy for record</param>
        public PagedDataTable(int pageIndex, int pageSize, int totalItemCount, string search, string orderBy, string sortBy)
        {
            PageSize = pageSize;
            TotalItemCount = totalItemCount;
            TotalPageCount = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
            CurrentPageIndex = pageIndex;
            StartRecordIndex = (CurrentPageIndex - 1) * PageSize + 1;
            EndRecordIndex = TotalItemCount > pageIndex * pageSize ? pageIndex * pageSize : TotalItemCount;
            SearchText = search;
            OrderBy = orderBy;
            SortBy = sortBy;
        }

        public PagedDataTable(int pageIndex, int pageSize, int totalItemCount, string search, string orderBy, string sortBy, string fromDate, string toDate)
        {
            PageSize = pageSize;
            TotalItemCount = totalItemCount;
            TotalPageCount = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
            CurrentPageIndex = pageIndex;
            StartRecordIndex = (CurrentPageIndex - 1) * PageSize + 1;
            EndRecordIndex = TotalItemCount > pageIndex * pageSize ? pageIndex * pageSize : TotalItemCount;
            SearchText = search;
            FromDate = fromDate;
            ToDate = toDate;
        }


        /// <summary>
        /// Current Page Index
        /// </summary>
        [DataMember]
        public int CurrentPageIndex { get; set; }
        /// <summary>
        /// page size
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }
        /// <summary>
        /// Total item count
        /// </summary>
        [DataMember]
        public int TotalItemCount { get; set; }
        /// <summary>
        /// Total Page count
        /// </summary>
        [DataMember]
        public int TotalPageCount { get; set; }
        /// <summary>
        /// start record index
        /// </summary>
        [DataMember]
        public int StartRecordIndex { get; set; }
        /// <summary>
        /// End record index
        /// </summary>
        [DataMember]
        public int EndRecordIndex { get; set; }
        /// <summary>
        /// Search Text in record
        /// </summary>
        [DataMember]
        public string SearchText { get; set; }

        [DataMember]
        public string PageAction { get; set; }

        [DataMember]
        public string FromDate { get; set; }
        [DataMember]
        public string ToDate { get; set; }

        /// <summary>
        /// order by in record
        /// </summary>
        [DataMember]
        public string OrderBy { get; set; }
        /// <summary>
        /// Sort by in record
        /// </summary>
        [DataMember]
        public string SortBy { get; set; }

        [DataMember]
        public bool Success{ get; set; }
        [DataMember]
        public string MessageText { get; set; }
    }
}
