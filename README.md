# Preview
Web API for stack storage for actions : pop,push,peek,revert

# Implementation
Using ASP.NET Core and Entity core ORM, web api was created while persistence data was kept on in memory database.

# Reversed Stack Web API:

Push : Request Type: POST, https://localhost:5001/api/StackItems/push, Body: {data:REQUIRED_DATA}

Pop : Request Type: GET, https://localhost:5001/api/StackItems/pop

Peek : Request Type: GET, https://localhost:5001/api/StackItems/peek

Revert: Request Type: POST, https://localhost:5001/api/StackItems/revert
