package rw.auca.lectrolend.scheduler;

import lombok.RequiredArgsConstructor;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Component;
import rw.auca.lectrolend.service.ReservationService;

/** Runs the reservation housekeeping (expire uncollected bookings, flag overdue items) every minute. */
@Component
@RequiredArgsConstructor
public class ReservationScheduler {

    private final ReservationService reservations;

    @Scheduled(fixedDelay = 60_000, initialDelay = 15_000)
    public void tick() {
        reservations.runHousekeeping();
    }
}
