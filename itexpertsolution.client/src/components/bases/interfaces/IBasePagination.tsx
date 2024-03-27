export default interface IBasePagination {
    pageCount: number,
    pageSize?: number,
    currentPage?: number,
    onPageChange: (page: number) => void
}