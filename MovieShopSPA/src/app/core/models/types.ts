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

export type Cast = {
  id: number;
  name: string;
  gender: string;
  tmdbUrl: string;
  profilePath: string;
  character: string;
};

export type MovieDetail = {
  id: number;
  title: string;
  overview: string;
  tagline: string;
  budget?: number;
  revenue?: number;
  imdbUrl: string;
  tmdbUrl: string;
  posterUrl: string;
  backdropUrl: string;
  originalLanguage: string;
  releaseDate: string;
  runTime: number;
  price: number;
  rating?: number;
  genres: Array<Genre>;
  casts: Array<Cast>;
};

