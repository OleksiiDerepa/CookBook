export interface CollectionApiResponse<T> {
    currentPage: number;
    pageSize: number;
    overallCount: number;
    pageCount: number;
    content: T[];
}
