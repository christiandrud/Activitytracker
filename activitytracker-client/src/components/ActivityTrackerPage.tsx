import axios from "axios";
import React, { useEffect } from "react";
import { Activity, ActivityCreate } from "../domain/domain";
import ActivityCard from "./ActivityCard";

const client = axios.create({
  baseURL: "http://localhost:5000/api",
});

export type ActivityProps = {
  activity: Activity;
};

export const Activty = (props: ActivityProps) => {
  return (
    <tr>
      <td className="border border-2">{props.activity.name}</td>
      <td className="border border-2">{props.activity.start}</td>
      <td className="border border-2">{props.activity.end}</td>
      <td className="border border-2">{props.activity.duration}</td>
    </tr>
  );
};

export const ActivityTrackerPage = () => {
  const currentUserId: string = "Mister Activity";

  const getActivities = async () => {
    const response = await client.get(
      "/activities/".concat(encodeURIComponent(currentUserId))
    );
    setActivities(response.data);
  };

  const handleStartActivity = async (activityName: string) => {
    if (activityName === "") {
      alert("ActivityName cannot be null");
    } else {
      const activityCreate: ActivityCreate = {
        username: currentUserId,
        name: activityName,
      };

      await client.post("/activity/create", activityCreate);
      await getActivities();
    }
  };

  const handleEndActivity = async (activityId: string) => {
    const url = "/activity/update/".concat(encodeURIComponent(activityId));
    await client.post(url);
    await getActivities();
  };

  const defaultActivities: Array<Activity> = [];
  const [activities, setActivities] = React.useState(defaultActivities);
  useEffect(() => {
    getActivities();
  }, [currentUserId, setActivities]);

  return (
    <div className="mt-8">
      <ActivityCard
        activity={activities.filter((x) => x.end === "")[0]}
        startActivity={handleStartActivity}
        endActivity={handleEndActivity}
      />
      <div className="flex flex-cols justify-center text-center mt-8">
        <div className="flex-auto w-1/4"></div>
        <div className="w-2/4">
          <table className="table-auto w-full border">
            <thead>
              <tr>
                <th className="border border-2">Name of activity</th>
                <th className="border border-2">Start</th>
                <th className="border border-2">Stop</th>
                <th className="border border-2">Duration</th>
              </tr>
            </thead>
            {activities.length > 0 && (
              <tbody>
                {activities.map((a) => (
                  <Activty key={a.activityId} activity={a} />
                ))}
              </tbody>
            )}
          </table>
        </div>
        <div className="w-1/4"></div>
      </div>
    </div>
  );
};
