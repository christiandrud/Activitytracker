import TopBar from "./components/TopBar";
import { ActivityTrackerPage } from "./components/ActivityTrackerPage";

function App() {
  return (
    <div>
      <div className="flex flex-col">
        <TopBar name="Mister Activity" id={1} />
        <ActivityTrackerPage />
      </div>
    </div>
  );
}

export default App;
