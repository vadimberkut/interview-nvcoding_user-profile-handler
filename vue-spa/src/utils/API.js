import Http from './http.js';

function urlBuilder() {
    return Array.prototype.join.call(arguments, '/')
}

function buildUrl(urlTemplate, endpoint, queryStringParams = {}, doEncodeURI = true, doEncodeURIComponents = true) {
    if (!endpoint)
        throw new Error('endpoint can\'t be empty');
    let url = urlTemplate.replace("<endpoint>", endpoint);
    if(doEncodeURI) {
        url = encodeURI(url);
    }

    // build query string
    if (queryStringParams && typeof queryStringParams === 'object') {
        let queryString = Object.keys(queryStringParams).reduce((res, key, i) => {
            if (queryStringParams[key] === null || queryStringParams[key] === undefined)
                return res;

            if (i === 0)
                res += '?';
            else
                res += '&';

            if(doEncodeURIComponents) {
                res += `${encodeURIComponent(key)}=${encodeURIComponent(queryStringParams[key])}`;
            }
            else {
                res += `${key}=${queryStringParams[key]}`;
            }

            return res;
        }, "");

        url = `${url}${queryString}`;
    }
    return url;
}

let serverUrl = window.location.origin;
if(window.location.origin === "http://localhost:8080") {
    // Dev
    serverUrl = "http://localhost:33477";
}
let apiUrl = `${serverUrl}/api`;

/**
 * Adds auth header to request before send
 * @param {object} axiosConfig
 * @return {object} response
 */
function axiosAuthRequest(axiosConfig) {
    return new Promise((resolve, reject) => {
        Http.axios(axiosConfig).then(response => resolve(response)).catch(err => {
            let { config, request, response } = err;
            reject(response.data); // reject with error response from server
        });
    });
}


/**
 * Handles base response from API.
 * 
 * @param {any} response 
 */
function handleApiResponse(response) {
    console.log(response);
    // Throw if not success status code
    if(response.status === 400) { // Validation error
        // throw new ServerValidationErrorModel(response);
        throw response;
    }
    if(response.status === 500) { // Server error
        // throw new ServerErrorModel(response);
        throw response;
    }
    if ( // Other cases
        !response ||
        (
            response.status !== 200 &&
            response.status !== 201
        )
    ) {
        // In this case response contains 
        // throw new ServerUnknownErrorModel(response);
        throw response;
    }

    // Success response
    return response.data;
}

export default class API {

    static helpers() {
        return {
            mapImageUrl: (url) => {
                if(url === null || url.length === 0) {
                    return '';
                }
                let separator = url.indexOf('/') === 0 ? '' : '/';
                let result = `${serverUrl}${separator}/${url}`;
                return result;
            }
        }
    }

    static userProfiles() {
        let url = `${apiUrl}/userprofiles/<endpoint>/`;
        return {
            getAll: (params) => {
                return axiosAuthRequest({ url: buildUrl(url, "getAll", params), method: "get" }).then((response) => {
                    response.data.data = response.data.data.map(x => {
                        x.imageUrlAbsolute = API.helpers().mapImageUrl(x.imageUrl);
                        return x;
                    });
                    return response.data;
                }).catch(handleApiResponse);
            },
            create: (profile) => {
                return axiosAuthRequest({ url: buildUrl(url, "create"), method: "post", data: profile }).then((response) => {
                    response.data.imageUrlAbsolute = API.helpers().mapImageUrl(response.data.imageUrl);
                    return response.data;
                }).catch(handleApiResponse);
            },
            update: (profile) => {
                return axiosAuthRequest({ url: buildUrl(url, "update"), method: "put", data: profile }).then((response) => {
                    response.data.imageUrlAbsolute = API.helpers().mapImageUrl(response.data.imageUrl);
                    return response.data;
                }).catch(handleApiResponse);
            },
            updateRole: (params) => {
                let { userProfileId, userProfileRoleId } = params;
                return axiosAuthRequest({ url: buildUrl(url, "updateRole"), method: "put", data: { userProfileId, userProfileRoleId } }).then(handleApiResponse).catch(handleApiResponse);
            },
            updateSettings: (userProfileSettings) => {
                return axiosAuthRequest({ url: buildUrl(url, "updateSettings"), method: "put", data: userProfileSettings }).then(handleApiResponse).catch(handleApiResponse);
            },
        }
    }
    static roles() {
        let url = `${apiUrl}/roles/<endpoint>/`;
        return {
            getAll: (params) => {
                return axiosAuthRequest({ url: buildUrl(url, "getAll", params), method: "get" }).then(handleApiResponse).catch(handleApiResponse);
            }
        }
    }
}