export interface QueryParams {
    page: number;
    pageSize: number;
    sortOrder: SortEnum | string;
    sortBy: string;
    search: string;
    operator: FilterOperatorEnum | string;
}

export interface QueryParamsRes<T> {
    data: T[] | null;
    queryParams: QueryParams | null;
    count: number;
    totalCount: number;
}

export enum SortEnum {
    ASC,
    DESC
}

export enum FilterOperatorEnum {
    Equals,
    DoesntEqual,
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    Contains,
    NotContains,
    StartsWith,
    EndsWith,
    IsEmpty,
    IsNotEmpty
}