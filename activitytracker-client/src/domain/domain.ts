import { z } from "zod";

export const ActivitySchema = z.object({
  activityId: z.string(),
  name: z.string(),
  start: z.string(),
  end: z.string(),
  duration: z.string(),
});

export const ActivityCreateSchema = z.object({
  username: z.string(),
  name: z.string(),
});

export type Activity = z.infer<typeof ActivitySchema>;
export type ActivityCreate = z.infer<typeof ActivityCreateSchema>;
