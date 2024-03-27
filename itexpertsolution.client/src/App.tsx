import { Solution } from './components/solution/components/Solution.tsx'
import './App.css'
import 'bootstrap/dist/css/bootstrap.min.css';
import { Tab, Tabs } from 'react-bootstrap';
import SolutionCard from './components/solution/components/SolutionCard.tsx';
function App() {
    return (
        <>
            <Tabs
                defaultActiveKey="list"
                transition={false}
                id="noanim-tab-example"
                className="mb-3"
            >
                <Tab eventKey="list" title="Список">
                    <Solution/>
                </Tab>
                <Tab eventKey="solutionCard" title="Ввод данных">
                    <SolutionCard />
                </Tab>
            </Tabs>
        </>
    )
}

export default App
