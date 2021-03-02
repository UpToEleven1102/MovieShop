export type PaginationResponse<T> = {
  pageSize: number;
  pageNumber: number;
  pageCount: number;
  data: T[];
};

export type Genre = {
  id: number;
  name: string;
};
