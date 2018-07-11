# EVIC
The EVE to iCal mapper.

The code is not that great but it works.

# How to install using docker-compose

* Create an application at your https://developers.eveonline.com/
* Give it the scope: esi-calendar.read_calendar_events
* Give it the callback url: {your_url}/api/Authorization/code
* Fill in the client_id, client_secret, base_url={your_url}
* Fill in the mongo db settings
* Think of an user-agent so that ccp can contact you if you fuck up (after which you contact me I guess)
* ```docker-compose up -d```
* Done
