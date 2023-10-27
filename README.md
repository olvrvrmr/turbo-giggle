# turbo-giggle

## Architecture
![Architecture diagram](./architecture.svg)

## Core Components & Data Flows:
### Core Components:
1. Sync Engine: Responsible for API requests and data transformation.
2. Scheduler: Manages the timing and triggering of tasks.
3. Database: Holds metadata, logs, and task configurations.
4. User Interface: For task configuration, monitoring, and management.
5. Configuration Manager: Manages environment variables and other settings.
6. Message Queue: Mediates communication between components.

### Data Flows:
User Interface → Message Queue
- User configures tasks and monitors statuses.
- Sends configuration and control commands to a topic in the message queue.

Message Queue → Configuration Manager
- Receives configuration from the queue and updates internal settings.
- May also persist configurations to the Database.

Configuration Manager → Message Queue → Scheduler
- Scheduler subscribes to a topic to receive scheduling configurations.
- Scheduler updates its task list based on the received configurations.

Scheduler → Message Queue → Sync Engine
- Scheduler publishes "trigger events" to a specific topic in the message queue.
- Sync Engine subscribes to this topic and performs tasks upon receiving a trigger event.

Sync Engine → Message Queue → Database
- After executing tasks, Sync Engine sends results and logs to a topic.
- Database service subscribes to this topic and updates records accordingly.

Sync Engine → Message Queue → User Interface
- Status updates and results are also sent to a topic the User Interface is subscribed to for real-time monitoring.


### Moving Parts of the Scheduler

#### 1. Trigger Mechanism
- **Functionality**: Responsible for initiating the tasks based on time or external triggers.
- **Inputs**: Configuration from the Config Manager, time, external triggers.
- **Outputs**: Trigger events pushed to the message queue.

#### 2. Configuration Loader
- **Functionality**: Loads task configurations from the Configuration Manager via the message queue.
- **Inputs**: Configuration messages from the message queue.
- **Outputs**: Task configurations to be consumed internally.

#### 3. Task Queue Manager
- **Functionality**: Manages a priority queue of tasks to be executed based on triggers.
- **Inputs**: Trigger events, task configurations.
- **Outputs**: Sorted queue of tasks.

#### 4. State Manager
- **Functionality**: Keeps track of the states of various tasks (e.g., pending, running, completed, failed).
- **Inputs**: Updates from the Sync Engine, initial states from the database.
- **Outputs**: State updates pushed to the message queue and database.

#### 5. Error Handler
- **Functionality**: Manages errors and failures, perhaps implementing retries or alerting mechanisms.
- **Inputs**: Error messages or failure states.
- **Outputs**: Retry tasks, alerts, or error logs.

#### 6. Logging and Monitoring
- **Functionality**: Logs events and keeps track of performance metrics.
- **Inputs**: Various events and states from other components.
- **Outputs**: Log files and metrics, potentially also pushed to a monitoring dashboard.

#### 7. Scheduler API
- **Functionality**: Optionally, an API to interact programmatically with the scheduler for advanced configurations or management.
- **Inputs**: API calls.
- **Outputs**: Configuration updates, task initiations, etc.

### Communication Flows

1. **Configuration Loader** fetches the configuration details from the **Configuration Manager** via the **Message Queue**.
2. **Trigger Mechanism** uses these configurations to create trigger events.
3. **Task Queue Manager** uses these trigger events to enqueue tasks.
4. **State Manager** keeps track of which tasks are running, succeeded, or failed.
5. **Error Handler** steps in if any task fails and decides on the course of action.
6. **Logging and Monitoring** keeps track of all activities for auditing and performance monitoring.
7. **Scheduler API** could be used for programmatic interactions.