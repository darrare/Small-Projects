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

    container.addEventListener('scroll', () => {
        if (!ticking) {
            window.requestAnimationFrame(() => {
                const iso = getVisibleWeek();
                if (iso) dotNetHelper.invokeMethodAsync('UpdateCurrentMonth', iso);
                ticking = false;
            });
            ticking = true;
        }
    });


    let targetScrollTop = container.scrollTop;
    const animationDuration = 150;

    const scrollStep = 150;
    container.addEventListener('wheer', (event) => {
        event.preventDefault();
        const scrollDirection = event.deltaY > 0 ? 1 : -1;
        targetScrollTop += (scrollDirection * scrollStep);

        const maxScroll = container.scrollHeight - container.clientHeight;
        targetScrollTop = Math.max(0, Math.min(maxScroll, targetScrollTop));

        smoothScrollTo(container, targetScrollTop, animationDuration);
        return;
    });
}

function smoothScrollTo(element, target, duation) {
    const start = element.scrollTop;
    const change = target - start;
    const startTime = performance.now();

    function animateScroll(currentTime) {
        const timeElapsed = currentTime - startTime;

        const progress = Math.min(timeElapsed / duration, 1);
        const easingProgress = progress * (2 - progress);

        element.scrollTop = start + change * easingProgress;

        if (timeElapsed < duration) {
            requestAnimationFrame(animateScroll);
        }
    }

    requestAnimationFrame(animateScroll);
}

export function scrollToIndex(container, index, itemHeight) {
    if (!container) return;
    const top = index * itemHeight;

    function attemptScroll(attempts = 0) {
        container.scrollTop = top;
        if (attempts < 100 && container.scrollTop == 0) {
            setTimeout(() => attemptScroll(attempts + 1), 10);
        }
    }

    attemptScroll();
}