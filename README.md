# Code2Gether discord group challenges

### June City finder
1. Get a api key and host from: https://rapidapi.com/saasindustries/api/zipcodebase-zip-code-search

2. Add a appsettings.json and configure:
`"ZipCodeBaseApiUri": "https://zipcodebase-zip-code-search.p.rapidapi.com/search?",
	"RapidApiKey": "Enter your key here",
	"RapidApiHost": "Enter your host here"`


(This is if you want to use IOptions.`"ZipCodeApiOptions": {
		"Uri": "",
		"RapidApiKey": "",
		"RapidApiHost": ""
	}`)
