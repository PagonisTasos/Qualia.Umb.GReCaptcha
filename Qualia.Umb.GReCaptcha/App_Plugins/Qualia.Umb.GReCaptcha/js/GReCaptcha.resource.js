function GReCaptchaResource($q, $http, umbRequestHelper) {

    return {
        getkeys: function () {
            return umbRequestHelper.resourcePromise(
                $http.get("/umbraco/backoffice/api/GReCaptcha/GetKeys"), "Failed to get keys");
        },
        savekeys: function (keys) {
            return umbRequestHelper.resourcePromise(
                $http.post("/umbraco/backoffice/api/GReCaptcha/SaveKeys", keys), "Failed to save keys");
        }
    };
}
angular.module("umbraco.resources").factory("GReCaptchaResource", GReCaptchaResource);