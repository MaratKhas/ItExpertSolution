import axios from "axios";
import { SyntheticEvent, useState } from "react";
import { Alert, Button, Col, Form, Row } from "react-bootstrap";
import { IBaseResponse } from "../../bases/interfaces/IBaseResponse.tsx";


export default function SolutionCard() {
    const [json, setJson] = useState('');
    const [showAlert, setShowAlert] = useState(false);
    const [message, setMessage] = useState('');
    const [messageVariant, setMessageVariant] = useState('')
    
    const onChangeJsonHandler = (event: React.ChangeEvent<HTMLInputElement>) => {
        setJson(event.target.value)
    };

    const onSubmitHandler = (e: SyntheticEvent) => {
        e.preventDefault();
        putData();
        console.log(json);
    };

    const readResponce = (response: IBaseResponse) => {
        setShowAlert(true)
        if (!response.success) {
            setMessage(response.error)
            setMessageVariant('danger')
        }
        else if (response.success) {
            setMessage('Данные успешно загружены')
            setMessageVariant('success')
        }
    };

    const putData = function () {
        axios.put("api/solutions/list", JSON.stringify(json), { headers: { "Content-Type": "application/json" } })
            .then((responce) => { readResponce(responce.data) })
            .catch((error) => console.log(error))
            .finally(() => { })
    }

    return (
        <>
            <h3>Ввод данных</h3>
            <Alert show={showAlert} onClose={() => setShowAlert(false)} variant={messageVariant}>
                {message }
            </Alert>
            <Form onSubmit={onSubmitHandler}>
                <Row className="">
                    <Form.Group as={Col} controlId="json">
                        <Button type="submit" title="Отправить данные">Отправить данные</Button>
                        <Row>
                            <Form.Label>Данные в виде JSON</Form.Label>
                            <Form.Control as="textarea" placeholder="JSON" name="json" value={json} onChange={onChangeJsonHandler} />
                            </Row>
                    </Form.Group>
                </Row>
            </Form>
        </>
    )
}