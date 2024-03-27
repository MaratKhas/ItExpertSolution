/* eslint-disable react-hooks/exhaustive-deps */
import React, { useEffect, useState } from 'react';
import IBasePagination from './interfaces/IBasePagination'
import Pagination from 'react-bootstrap/Pagination';
import { Row } from 'react-bootstrap';

const BasePagination: React.FC<IBasePagination> = (props: IBasePagination) => { 

    const currentPage =props.currentPage as number
    const pageCount = props.pageCount as number

    const handlePageChange = (page: number) => {
        props.onPageChange(page);
    };

    const goToLeft = (currentPage: number) => {
        handlePageChange(--currentPage)
    }
    const goToRight = (currentPage: number) => {
        handlePageChange(++currentPage)
    }

    const goToFirst = (currentPage: number) => {
        handlePageChange(currentPage)
    }

    const goToLast = (currentPage: number) => {
        handlePageChange(currentPage)
    }

    const goToLeftDisabled = currentPage === 1;
    const goToRightDisabled = currentPage === pageCount;

    const [currentRangeStart, setCurrentRangeStart] = useState(0);
    const [currentRangeEnd, setCurrentRangeEnd] = useState(0);

    useEffect(() => {
        let rangeStart = 1;
        let rangeEnd = pageCount;
        if (currentPage > rangeEnd)
            handlePageChange(rangeEnd)
        if (rangeEnd > 10) {
            rangeStart = Math.max(1, currentPage - 4);
            rangeEnd = Math.min(pageCount, currentPage + 5)
            if (rangeStart === 1) {
                rangeEnd = rangeStart + 9
            } else if (rangeEnd == pageCount) {
                rangeEnd = Math.min(pageCount, rangeStart + 9);
            }

        }
        setCurrentRangeStart(rangeStart)
        setCurrentRangeEnd(rangeEnd)
    }, [currentPage, currentRangeEnd, pageCount])

    const pageNumbers = [];
    if (props.pageCount) {
        if (currentPage > props.pageCount) {
            props.onPageChange(props.pageCount)
        }
        for (let i = currentRangeStart; i <= currentRangeEnd; i++) {
            pageNumbers.push(
                <Pagination.Item key={i} active={i === currentPage} disabled={i === currentPage} onClick={() => handlePageChange(i)}>
                    {i}
                </Pagination.Item>
            );
        }
    }
    return (
        <>
            <Row >
                <Pagination className="justify-content-center">
                    <Pagination.First disabled={goToLeftDisabled} onClick={() => goToFirst(1)} />
                    <Pagination.Prev disabled={goToLeftDisabled} onClick={() => goToLeft(currentPage)} />
                    {pageNumbers}
                    <Pagination.Next disabled={goToRightDisabled} onClick={() => goToRight(currentPage)} />
                    <Pagination.Last disabled={goToRightDisabled} onClick={() => goToLast(pageCount)} />
                </Pagination>
            </Row>
        </>
    );
}

export default BasePagination;