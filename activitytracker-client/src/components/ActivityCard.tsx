import React, { useEffect, ChangeEvent, useCallback } from "react";
import { Activity } from "../domain/domain";

export type ActivityCardProps = {
  activity: Activity;
  startActivity: (activityName: string) => void;
  endActivity: (activityId: string) => void;
};

export const ActivityCard = (props: ActivityCardProps) => {
  const [activityName, setActivityName] = React.useState<string>("");
  const [activityNameRequired, setActivityNameRequired] =
    React.useState<boolean>(false);

  const onInputActivityNameChange = (e: ChangeEvent<HTMLInputElement>) => {
    setActivityName(e.target.value);
    if (e.target.value === "") {
      setActivityNameRequired(true);
    } else {
      setActivityNameRequired(false);
    }
  };

  const onStop = () => {
    setActivityNameRequired(false);
    setActivityName("");
    props.endActivity(props.activity.activityId);
  };

  const onStart = useCallback(() => {
    if (activityName === undefined || activityName === "") {
      setActivityNameRequired(true);
    } else {
      props.startActivity(activityName);
    }
  }, [activityName, props]);

  const [enableStartButton, setEnableStartButton] =
    React.useState<boolean>(true);

  useEffect(() => {
    setEnableStartButton(props.activity === undefined);
    if (
      props.activity !== undefined &&
      props.activity.end !== "" &&
      activityName === ""
    ) {
      setActivityName(props.activity.name);
    }
  }, [props.activity, activityName]);

  return (
    <div className="flex justify-center text-center">
      <div className="flex-auto w-4/12"></div>
      <div className="flex-auto w-4/12 rounded border border-black bg-gray-100 p-4">
        <div className="pb-4">
          <span className="pr-4">Activity name:</span>
          <input
            value={activityName}
            readOnly={!enableStartButton}
            type="text"
            className="border border-black"
            onChange={onInputActivityNameChange}
            id="inputActivityCard"
          />
          {activityNameRequired && (
            <span className="text-red-500 font-bold ml-1">*Required</span>
          )}
        </div>
        <div className="flex flex-cols justify-center text-center">
          {enableStartButton && (
            <div
              id="divStart"
              className="p-2 bg-lime-600 w-20 rounded cursor-pointer"
              onClick={() => onStart()}
            >
              Start
            </div>
          )}
          {!enableStartButton && (
            <div id="divStart" className="p-2 bg-stone-200 w-20 rounded">
              Start
            </div>
          )}
          <div className="w-2"></div>
          {!enableStartButton && (
            <div
              id="divStop"
              className="p-2 bg-red-400 w-20 rounded cursor-pointer"
              onClick={() => onStop()}
            >
              Stop
            </div>
          )}
          {enableStartButton && (
            <div id="divStop" className="p-2 bg-stone-200 w-20 rounded">
              Stop
            </div>
          )}
        </div>
      </div>
      <div className="flex-auto w-4/12"></div>
    </div>
  );
};

export default ActivityCard;
