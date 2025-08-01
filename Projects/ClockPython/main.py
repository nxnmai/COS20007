from Clock import Clock
import os
import psutil

if __name__ == "__main__":
    print("Creating a new clock instance")
    clock = Clock()

    print(f"Initial time: {clock.Time}")

    print("Ticking the clock 5 times")
    for i in range(5):
        clock.Tick()
        print(f"After tick {i + 1}: {clock.Time}")

    print("Resetting the clock")
    clock.Reset()
    print(f"After reset: {clock.Time}")

    # Get the current process
    proc = psutil.Process(os.getpid())
    print(f"Current process: {proc.name()}")

    # Display the total physical memory size allocated for the current process
    print(f"Physical memory usage: {proc.memory_info().rss} bytes")

    # Display peak memory statistics for the process
    print(f"Peak physical memory usage: {proc.memory_info().vms} bytes")
    