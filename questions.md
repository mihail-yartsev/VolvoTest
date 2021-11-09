# Questions and comments for the task

## Questions (with assumed answers)
- That's considered the end of the day?
	- I'm going to assume it's 00:00 UTC (for simplicity).
- What happens if the vehicle passes several times inside the 60 min window, but the window spans across the day border? 
	- I'm going to assume that the vehicle is still charged once, and the sum goes into the amount of the second day.
- How to combine multiple passes into a single 60-min window if there is more than one option to do it?
	- Example: there are 5 passes inside 75 mins window with 15 mins intervals in between with the following fees for each: 15, 10, 10, 15, 10
	- The task says "vehicle that passes several tolling stations within 60 minutes is only taxed once. The amount that must be paid is the highest one"
	- If understood literally this would mean that passes should be combined in the way which makes the highest payment, so in the example above it should be 15 (for the first pass) + 15 (for the following 4)
	- This would though require a complex algorithm, too obscure for the drivers and not user-friendly. The idea of the tax is to motivate people to drive less during peak hours, not to confuse them. 
	- So I will choose to use a simpler option: start the time window simply where another one has ended. In the example above this would result in 15 (for the first 4 passes) + 10 (for the last pass).
- How to expose the code best as an api and how will it be used?
	- I'm going to assume that there's another system that stores all the passes and sends the not billed days on schedule to this calculator api. I will expose it via GRPc (fine for an internal service). I will assume that system is to send one vehicle data per request and always sends data for the whole day, but might send data for several days.

## If I had more time for this task 
I would have:
- Spend more time testing, ex separate the test into several methods testing different business cases. Right now it's all in one test method which is not maintainable but works to test basic correctness.
- Replace tuples with proper types
- Add proper error handling
- Fetch the settings from the external source. Depending on the scenario, it could be a appsettings.json file, a db, or a configuration admin system api (if we want to follow a database per service pattern).
- E2E tests including client-server communication would have been nice.
- A second, rest api would be nice to ease the access to the calculation 
- Implement some deployment capability, ex pack to docker container to run on k8s
- Add observability: metrics, tracing, logs

## How to test localy
1. Download the archive from the releases page on github and unzip it
1. Run the .exe 
1. Run grpc-ui/run-ui.cmd

## Dependencies and building
You'll need to have .net sdk 5 to build the project.