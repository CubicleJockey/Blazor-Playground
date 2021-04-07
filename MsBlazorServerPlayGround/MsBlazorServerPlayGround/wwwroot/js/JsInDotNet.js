window.CallMeInDotNet = (date) => {
    if (date) {
        const message = `A .NET Method called me on [${date}].`;
        console.log(message);
        return message;
    } else {
        return 'Something went wrong.';
    }
};