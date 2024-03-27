import { ISolution } from "../../solution/interfaces/ISolution";
import IBasePagination  from "./IBasePagination";

export default interface IBasePaginationList extends IBasePagination {
    items?: ISolution[] | null
}