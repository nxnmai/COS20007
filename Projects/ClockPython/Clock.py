import psutil
import os

class Counter:
    def __init__(self, name):
        self._name = name
        self._count = 0

    def Increment(self):
        self._count += 1

    def Reset(self):
        self._count = 0

    def Reset_by_default(self):
        try:
            self._count = 0
        except OverflowError as ex:
            print(f"Overflow error: {ex}")

    @property
    def name(self):
        return self._name

    @name.setter
    def name(self, value):
        self._name = value

    @property
    def ticks(self):
        return self._count


class Clock:
    def __init__(self, is_24_hour_format=None):
        if is_24_hour_format is None:
            student_id = input("Please enter your student ID: ")
            if not student_id:
                raise ValueError("Student ID cannot be null or empty.")
            last_digit = int(student_id) % 10
            self._is_24_hour_format = last_digit > 5
        else:
            self._is_24_hour_format = is_24_hour_format

        self._hours = Counter("hours")
        self._minutes = Counter("minutes")
        self._seconds = Counter("seconds")

    def Tick(self):
        self._seconds.Increment()
        if self._seconds.ticks >= 60:
            self._seconds.Reset()
            self._minutes.Increment()
            if self._minutes.ticks >= 60:
                self._minutes.Reset()
                self._hours.Increment()
                max_hours = 24
                if self._hours.ticks >= max_hours:
                    self._hours.Reset()

    def Reset(self):
        self._hours.Reset()
        self._minutes.Reset()
        self._seconds.Reset()

    @property
    def time(self):
        if self._is_24_hour_format:
            return f"{self._hours.ticks:02d}:{self._minutes.ticks:02d}:{self._seconds.ticks:02d}"
        else:
            display_hours = self._hours.ticks % 12
            if display_hours == 0:
                display_hours = 12
            period = "AM" if self._hours.ticks < 12 else "PM"
            return f"{display_hours:02d}:{self._minutes.ticks:02d}:{self._seconds.ticks:02d} {period}"

    @property
    def hours(self):
        return self._hours.ticks

    @property
    def minutes(self):
        return self._minutes.ticks

    @property
    def seconds(self):
        return self._seconds.ticks


if __name__ == "__main__":
    print("Creating a new clock instance")
    clock = Clock()

    print(f"Initial time: {clock.time}")

    print("Ticking the clock 5 times")
    for i in range(5):
        clock.Tick()
        print(f"After tick {i + 1}: {clock.time}")

    print("Resetting the clock")
    clock.Reset()
    print(f"After reset: {clock.time}")

    # Get the current process
    proc = psutil.Process(os.getpid())
    print(f"Current process: {proc.name()}")

    # Display the total physical memory size allocated for the current process
    print(f"Physical memory usage: {proc.memory_info().rss} bytes")

    # Display peak memory statistics for the process
    print(f"Peak physical memory usage: {proc.memory_info().vms} bytes")