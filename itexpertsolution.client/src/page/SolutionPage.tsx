import 'bootstrap/dist/css/bootstrap.min.css';
import { Tab, Tabs } from 'react-bootstrap';
import SolutionCard from '../components/solution/components/SolutionCard.tsx';
import  Solution  from '../components/solution/components/Solution.tsx'
export default function SolutionPage() {
    return (
        <Tabs
            defaultActiveKey="list"
            transition={false}
            id="noanim-tab-example"
            className="mb-3"
        >
            <Tab eventKey="list" title="Список">
                <Solution />
            </Tab>
            <Tab eventKey="solutionCard" title="Ввод данных">
                <SolutionCard />
            </Tab>
        </Tabs>
    )
}