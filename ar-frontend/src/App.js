import LeaderBoard from "./Components/LeaderBoard";
import TopBar from './Components/TopBar';
import './App.css';

function App() {
  return (
    <div className="App">
      <TopBar />
      <main className="App-header">
        <LeaderBoard />
      </main>
    </div>
  );
}

export default App;
