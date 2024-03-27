

--Первый скрипт

select  clients.ClientName ,count(clientContacts.ContactValue) from Clients clients
left join ClientContacts  clientContacts on clientContacts.ClientId = clients.Id 
group by clients.ClientName


--Второй скрипт

select  clients.* from Clients clients
where clients.id in 
(
	select clientContacts.ClientID   from ClientContacts clientContacts
	group by clientContacts.ClientID
	having count(clientContacts.ClientID) > 2
)