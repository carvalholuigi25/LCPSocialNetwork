import { fetchingData, getMyApiUrl } from "../scripts/my_functions"
import { GetNotifications } from "./notifications";

const apiUrl = getMyApiUrl();

function Create(apiname = "posts", data, token) {
    return fetchingData(`${apiUrl}/api/${apiname}`, "POST", data, token, true);
}

function Read(apiname = "posts", id = -1, token) {
    var mid = id != -1 ? `/${id}`: '';
    return fetchingData(`${apiUrl}/api/${apiname}${mid}`, "GET", token, true);
}

function Update(apiname = "posts", id, data, token) {
    var mid = id != -1 ? `/${id}`: '';
    return fetchingData(`${apiUrl}/api/${apiname}${mid}`, "PUT", data, token, true);
}

function Delete(apiname = "posts", id, token) {
    var mid = id != -1 ? `/${id}`: '';
    return fetchingData(`${apiUrl}/api/${apiname}${mid}`, "DELETE", token, true);
}

export { Create, Read, Update, Delete }