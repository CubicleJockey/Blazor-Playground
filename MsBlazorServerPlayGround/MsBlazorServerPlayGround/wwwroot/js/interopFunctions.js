window.dotnetInJs = {
    getIntegers: function () {
        DotNet.invokeMethodAsync('MsBlazorServerPlayGround', 'GetListOfIntegers')
            .then(data => {
                document.getElementById('integers').innerText = data.join(', ');
            });
    },
    getSentenceAlias: function () {
        DotNet.invokeMethodAsync('MsBlazorServerPlayGround', 'GimmeASentence')
            .then(data => {
                document.getElementById('sentence-csharp-alias').innerText = data;
            });
    }
};