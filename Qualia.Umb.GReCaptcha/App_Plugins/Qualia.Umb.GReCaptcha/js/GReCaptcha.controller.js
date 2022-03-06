function GReCaptchaController(GReCaptchaResource, notificationsService) {

    var vm = this;

    vm.getkeys = getkeys;
    vm.savekeys = savekeys;
    vm.secretkey = '';
    vm.sitekey = '';
    getkeys();

    function getkeys() {
        GReCaptchaResource.getkeys().then(
            (result) => { vm.secretkey = result.secretkey; vm.sitekey = result.sitekey; },
            () => notificationsService.error("fetching keys failed", "the key values could not be fetched from storage!")
        );
    }
    function savekeys() {
        GReCaptchaResource.savekeys({ sitekey: vm.sitekey, secretkey: vm.secretkey }).then(
            (result) => notificationsService.success("keys saved", "the key values were stored successfully"),
            () => notificationsService.error("saving keys failed", "the key values were NOT stored")
        );
    }
}

angular.module("umbraco").controller("GReCaptchaController", GReCaptchaController);