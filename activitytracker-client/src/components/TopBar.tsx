export interface TopBarProps {
  id: number;
  name: string;
}

export const TopBar = ({ name }: TopBarProps) => {
  return (
    <div className="flex flex-auto flex-cols">
      <div className="bg-black text-white p-3">Activity Tracker V1</div>
      <div className="flex-auto bg-black h-14 text-right text-white">
        <div className="p-3">{name}</div>
      </div>
    </div>
  );
};

export default TopBar;
