export function initializeScrollListener(container, dotNetHelper) {
    if (!container) return;

    let ticking = false;
    const getVisibleWeek = () => {
        const mid = container.scrollTop + container.clientHeight / 2;
        const weekDivs = container.querySelectorAll('.week-row');
        for (let div of weekDivs) {
            const offsetTop = div.offsetTop;
            const height = div.offsetHeight;
            if (mid >= offsetTop && mid < offsetTop + height) {
                return div.getAttribute('data-week-start');
            }
        }
        return null;
    };

    container.addEventListener('scroll', function () {
        if (!ticking) {
            window.requestAnimationFrame(() => {
                const iso = getVisibleWeek();
                if (iso) dotNetHelper.invokeMethodAsync('UpdateCurrentMonth', iso);
                ticking = false;
            });
            ticking = true;
        }
    });

    const initialIso = getVisibleWeek();
    if (initialIso) dotNetHelper.invokeMethodAsync('UpdateCurrentMonth', initialIso);
}

export function scrollToIndex(container, index, itemHeight) {
    if (!container) return;
    const top = index * itemHeight;
    container.scrollTop = top - (container.clientHeight / 2) + (itemHeight / 2);
}
