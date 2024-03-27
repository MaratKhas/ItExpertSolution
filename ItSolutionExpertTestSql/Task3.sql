--Создаем обобщенную таблицу где для каждой даты одного и того же айдишника будет номер строки 
WITH numbered_dates AS (
    SELECT Id, Dt,
           ROW_NUMBER() OVER (PARTITION BY Id ORDER BY Dt) AS rn
    FROM Dates
)
--Джоиним обобщенную таблицу саму в себя по айди и номер строки первой таблицы = предыдущая из второй таблицы
--полученные даты слева и с права будет интервалом для определенного id
SELECT a.Id, a.Dt AS [start_date], b.Dt AS [end_date]
FROM numbered_dates a
JOIN numbered_dates b ON a.Id = b.Id AND a.rn = b.rn - 1
ORDER BY a.Id, a.Dt;