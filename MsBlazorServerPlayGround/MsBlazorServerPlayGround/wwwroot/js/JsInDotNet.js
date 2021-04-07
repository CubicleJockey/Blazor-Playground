window.CallMeInDotNet = (date) => {
    if (date) {
        const message = `A .NET Method called me on [${date}].`;
        console.log(message);
        return message;
    } else {
        return 'Something went wrong.';
    }
};

window.CallMeInAClass = (items) => {
    if (items) {
        const message = [];

        message.push(`Unsorted: ${items.join(',')}`);

        items.sort();
        message.push(`Sorted: ${items.join(',')}`);


        return message.join('<br/>');
    } else {
        return 'Collection was empty.';
    }
};