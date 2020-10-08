import { Column } from "./column";

export abstract class BaseListComponent {

    protected columns: Column[];
    queryResult: any = {
    };

    protected onInit(): void {
        this.populateList();
    }


    protected abstract populateList();

}
