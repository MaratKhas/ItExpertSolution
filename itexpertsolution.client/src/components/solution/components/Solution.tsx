import Table from 'react-bootstrap/Table'
import BasePagination from '../../bases/BasePaginationComponent.tsx'
import { Col, Form, Row } from "react-bootstrap";
import { useEffect, useState } from 'react';
import React from 'react';
import IBasePaginationList from '../../bases/interfaces/IBasePaginationList.tsx';
import axios from 'axios';
import './Solution.css'

interface IBaseFilter {
    code: number | null
    value: string | null
    ordering: number | null
    sortField: string | null
    isDesc: boolean | null
    currentPageNumber: number
    pageSize: number
}

export default function Solution() {
    const initialState = { code: null, value: null, ordering: null, sortField: null, isDesc: null, currentPageNumber: 1, pageSize: 10 };
    const [filtersState, setFiltersState] = useState<IBaseFilter>(initialState)
    const [data, setData] = useState<IBasePaginationList>();
    const [isLoading, setIsLoading] = useState(false);

    const feachData = function (filters: IBaseFilter) {
        setIsLoading(true)
        axios.post("api/solutions/list", JSON.stringify(filters), { headers: { "Content-Type": "application/json" } })
            .then((responce) => setData(responce.data))
            .catch((error) => console.log(error))
            .finally(() => setIsLoading(false))
    }

    const handleFilterCodeChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setFiltersState({ ...filtersState, code: event.target.value !== '' ? parseInt(event.target.value) : null, currentPageNumber: 1 });
    };

    const handleFilterValueChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setFiltersState({ ...filtersState, value: event.target.value !== '' ? event.target.value : null, currentPageNumber: 1 });
    };

    const handleFilterOrderingChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setFiltersState({ ...filtersState, ordering: event.target.value !== '' ? parseInt(event.target.value) : null, currentPageNumber: 1 });
    };
    const handleFilterPageSizeChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setFiltersState({ ...filtersState, pageSize: parseInt(event.target.value), currentPageNumber: 1 });
    };

    const handleSortFuildChange = (fuildName: string) => {
        let name = filtersState.sortField;
        let isDesc = filtersState.isDesc;

        if (name == null || name != fuildName) {
            name = fuildName
            isDesc = true
        }
        else if (name == fuildName && isDesc == true) {
            isDesc = !isDesc
        }
        else {
            name = null;
            isDesc = null;
        }

        setFiltersState({ ...filtersState, sortField: name, isDesc })
    }

    const getSortChar = (columnName: string) => {
        return filtersState.sortField == columnName && filtersState.isDesc != null ? filtersState.isDesc ? '↑' : '↓' : ''
    }

    const handleCurrentPageChange = async (page: number) => {
        setFiltersState((prevFilters) => ({
            ...prevFilters,
            currentPageNumber: page
        }))
    };

    useEffect(() => {
        feachData(filtersState)
    }, [filtersState, setFiltersState]);

    return (
        <>
            <Row>
                <Form>
                    <Row className="mb-3">
                        <Form.Group as={Col} controlId="code">
                            <Form.Label>Код</Form.Label>
                            <Form.Control placeholder="Код" name="code" type="number" value={filtersState.code !== null ? filtersState.code.toString() : ''} onChange={handleFilterCodeChange} />
                        </Form.Group>

                        <Form.Group as={Col} controlId="value">
                            <Form.Label>Значение</Form.Label>
                            <Form.Control placeholder="Значение" name="value" value={filtersState.value !== null ? filtersState.value : ''} onChange={handleFilterValueChange} />
                        </Form.Group>
                    </Row>
                    <Row className="mb-3">
                        <Form.Group as={Col} className="mb-3" controlId="ordering">
                            <Form.Label>Порядок</Form.Label>
                            <Form.Control placeholder="Порядок" name="ordering" type="number" value={filtersState.ordering !== null ? filtersState.ordering.toString() : ''} onChange={handleFilterOrderingChange} />
                        </Form.Group>
                        <Form.Group as={Col} className="mb-3" controlId="ordering">
                            <Form.Label>Выводить</Form.Label>
                            <Form.Select name="ordering" value={filtersState.pageSize} onChange={handleFilterPageSizeChange}>
                                <option value={10}>10</option>
                                <option value={25}>25</option>
                                <option value={50}>50</option>
                                <option value={100}>100</option>
                            </Form.Select>
                        </Form.Group>
                    </Row>
                </Form >
                {!isLoading ?
                    data?.items != null && data?.items?.length > 0 ?
                        <div>
                            <Table bordered>
                                <thead>
                                    <tr>
                                        <td className="pointer" onClick={() => handleSortFuildChange('Code')}>Код {getSortChar('Code')} </td>
                                        <td className="pointer" onClick={() => handleSortFuildChange('Value')}>Значение {getSortChar('Value')}</td>
                                        <td className="pointer" onClick={() => handleSortFuildChange('Ordering')}>Порядок {getSortChar('Ordering')}</td>
                                    </tr>
                                </thead>
                                <tbody>
                                    {data?.items?.map(item => (
                                        <tr key={item.id.toString()}>
                                            <td>{item.code}</td>
                                            <td>{item.value}</td>
                                            <td>{item.ordering}</td>
                                        </tr>
                                    ))}

                                </tbody>
                            </Table>
                            <BasePagination pageCount={data?.pageCount} pageSize={filtersState.pageSize} currentPage={filtersState.currentPageNumber} onPageChange={handleCurrentPageChange} />
                        </div>
                        :
                        <div>Данных нет</div>
                    :
                    <div>Загрузка данных</div>
                }
            </Row>
        </>
    );

}
