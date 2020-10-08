export interface Column {
    title: string;
    key: string;
    isSortable: boolean;
    isFilterable: boolean;
    filterValue: string;
    permissions?: string[];
}
